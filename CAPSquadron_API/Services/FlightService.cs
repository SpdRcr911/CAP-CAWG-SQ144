﻿using CAPSquadron_API.Data;
using CAPSquadron_API.Exceptions;
using CAPSquadron_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPSquadron_API.Services;

public class FlightService : IFlightService
{
    private readonly AppDbContext _context;

    public FlightService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FlightDto>> GetFlightsAsync()
    {
        var flights = await _context.Flights.ToListAsync();
        var flightDtos = new List<FlightDto>();

        foreach (var flight in flights)
        {
            var flightMembers = await _context.FlightMembers.Where(fm => fm.FlightId == flight.Id).ToListAsync();
            flightDtos.Add(new FlightDto
            {
                Id = flight.Id,
                Name = flight.Name,
                FlightCommanderId = flightMembers.FirstOrDefault(fm => fm.IsFlightCommander)?.CAPID,
                FlightSergeantIds = flightMembers.Where(fm => fm.IsFlightSergeant).Select(fm => fm.CAPID).ToList(),
                MemberIds = flightMembers.Select(fm => fm.CAPID).ToList()
            });
        }

        return flightDtos;
    }

    public async Task<FlightDto> GetFlightAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

        var flightMembers = await _context.FlightMembers.Where(fm => fm.FlightId == id).ToListAsync();

        return new FlightDto
        {
            Id = flight.Id,
            Name = flight.Name,
            FlightCommanderId = flightMembers.FirstOrDefault(fm => fm.IsFlightCommander)?.CAPID,
            FlightSergeantIds = flightMembers.Where(fm => fm.IsFlightSergeant).Select(fm => fm.CAPID).ToList(),
            MemberIds = flightMembers.Select(fm => fm.CAPID).ToList()
        };
    }

    public async Task<FlightDto> CreateFlightAsync(FlightDto flightDto)
    {
        var flight = new Flight { Name = flightDto.Name };

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        var flightMembers = new List<FlightMember>();

        if (flightDto.FlightCommanderId.HasValue)
        {
            flightMembers.Add(new FlightMember
            {
                FlightId = flight.Id,
                CAPID = flightDto.FlightCommanderId.Value,
                IsFlightCommander = true
            });
        }

        flightMembers.AddRange(flightDto.FlightSergeantIds.Select(id => new FlightMember
        {
            FlightId = flight.Id,
            CAPID = id,
            IsFlightSergeant = true
        }));

        flightMembers.AddRange(flightDto.MemberIds.Select(id => new FlightMember
        {
            FlightId = flight.Id,
            CAPID = id
        }));

        _context.FlightMembers.AddRange(flightMembers);
        await _context.SaveChangesAsync();

        flightDto.Id = flight.Id;
        return flightDto;
    }

    public async Task<FlightDto> UpdateFlightAsync(int id, FlightDto flightDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var flight = await _context.Flights.FindAsync(id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

            flight.Name = flightDto.Name;
            _context.Flights.Update(flight);

            var existingMembers = await _context.FlightMembers
                .Where(fm => fm.FlightId == id)
                .ToListAsync();

            _context.FlightMembers.RemoveRange(existingMembers);

            var flightMembers = new List<FlightMember>();

            var allMemberIds = new List<int>();
            if (flightDto.FlightCommanderId.HasValue)
            {
                allMemberIds.Add(flightDto.FlightCommanderId.Value);
            }

            allMemberIds.AddRange(flightDto.FlightSergeantIds);
            allMemberIds.AddRange(flightDto.MemberIds);

            // Ensure all CAPIDs exist in the attendance_sign_ins table
            var existingCapIds = await _context.Members
                .Where(a => allMemberIds.Contains(a.CAPID))
                .Select(a => a.CAPID)
                .ToListAsync();

            var missingCapIds = allMemberIds.Except(existingCapIds).ToList();
            if (missingCapIds.Count != 0)
            {
                throw new ArgumentOutOfRangeException($"Some CAPIDs are not members: {string.Join(", ", missingCapIds)}");
            }

            if (flightDto.FlightCommanderId.HasValue)
            {
                flightMembers.Add(new FlightMember
                {
                    FlightId = flight.Id,
                    CAPID = flightDto.FlightCommanderId.Value,
                    IsFlightCommander = true
                });
            }

            flightMembers.AddRange(flightDto.FlightSergeantIds.Select(id => new FlightMember
            {
                FlightId = flight.Id,
                CAPID = id,
                IsFlightSergeant = true
            }));

            flightMembers.AddRange(flightDto.MemberIds.Select(id => new FlightMember
            {
                FlightId = flight.Id,
                CAPID = id
            }));

            _context.FlightMembers.AddRange(flightMembers);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return flightDto;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteFlightAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

        var flightMembers = await _context.FlightMembers.Where(fm => fm.FlightId == id).ToListAsync();
        _context.FlightMembers.RemoveRange(flightMembers);
        _context.Flights.Remove(flight);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<int>> GetUnassignedCadetsAsync()
    {
        var assignedCadetCapIds = await _context.FlightMembers.Select(fm => fm.CAPID).Distinct().ToListAsync();
        var allCadetCapIds = await _context.Members.Where(m=> string.IsNullOrEmpty(m.FBIStatus) && m.Expiration > DateOnly.FromDateTime(DateTime.Now)).Select(m => m.CAPID).ToListAsync();

        return allCadetCapIds.Except(assignedCadetCapIds);
    }

    public async Task<FlightDetailDto> GetFlightDetailAsync(int id)
    {
        var flight = await _context.Flights
            .Include(f => f.FlightMembers)
            .ThenInclude(fm => fm.Member)
            .FirstOrDefaultAsync(f => f.Id == id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

        var flightDetail = new FlightDetailDto
        {
            Id = flight.Id,
            Name = flight.Name,
            FlightCommander = flight.FlightMembers
                .Where(fm => fm.IsFlightCommander)
                .Select(fm => new FlightMemberDto
                {
                    Capid = fm.CAPID,
                    Name = fm.Member?.FullName,
                    Rank = fm.Member?.Rank
                }).FirstOrDefault(),
            FlightSergeants = flight.FlightMembers
                .Where(fm => fm.IsFlightSergeant)
                .Select(fm => new FlightMemberDto
                {
                    Capid = fm.CAPID,
                    Name = fm.Member?.FullName,
                    Rank = fm.Member?.Rank
                }).ToList(),
            Members = flight.FlightMembers
                .Where(fm => !fm.IsFlightCommander && !fm.IsFlightSergeant)
                .Select(fm => new FlightMemberDto
                {
                    Capid = fm.CAPID,
                    Name = fm.Member?.FullName,
                    Rank = fm.Member?.Rank
                }).ToList()
        };

        return flightDetail;
    }
}
