using CAPSquadron_API.Data;
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
        var flight = await _context.Flights.FindAsync(id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

        flight.Name = flightDto.Name;
        _context.Flights.Update(flight);

        var existingMembers = await _context.FlightMembers.Where(fm => fm.FlightId == id).ToListAsync();
        _context.FlightMembers.RemoveRange(existingMembers);

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

        return flightDto;
    }

    public async Task DeleteFlightAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

        var flightMembers = await _context.FlightMembers.Where(fm => fm.FlightId == id).ToListAsync();
        _context.FlightMembers.RemoveRange(flightMembers);
        _context.Flights.Remove(flight);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<int>> GetUnassignedOrCommandersOrSergeantsAsync()
    {
        var assignedCapIds = await _context.FlightMembers.Select(fm => fm.CAPID).Distinct().ToListAsync();
        var allCapIds = await _context.Members.Select(m => m.CAPID).ToListAsync();

        return allCapIds.Except(assignedCapIds);
    }
}
