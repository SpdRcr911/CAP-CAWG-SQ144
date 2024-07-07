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
        return await _context.Flights
            .Select(f => new FlightDto
            {
                Id = f.Id,
                Name = f.Name,
                FlightCommanderId = f.FlightCommanderCAPID,
                FlightSergeantIds = f.FlightSergeants.Select(fs => fs.CAPID).ToList(),
                MemberIds = f.Members.Select(m => m.CAPID).ToList()
            }).ToListAsync();
    }

    public async Task<FlightDto> GetFlightAsync(int id)
    {
        var flight = await _context.Flights
            .Include(f => f.FlightSergeants)
            .Include(f => f.Members)
            .FirstOrDefaultAsync(f => f.Id == id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

        return new FlightDto
        {
            Id = flight.Id,
            Name = flight.Name,
            FlightCommanderId = flight.FlightCommanderCAPID,
            FlightSergeantIds = flight.FlightSergeants.Select(fs => fs.CAPID).ToList(),
            MemberIds = flight.Members.Select(m => m.CAPID).ToList()
        };
    }

    public async Task<FlightDto> CreateFlightAsync(FlightDto flightDto)
    {
        var flight = new Flight
        {
            Name = flightDto.Name,
            FlightCommanderCAPID = flightDto.FlightCommanderId,
            FlightSergeants = await _context.Members.Where(m => flightDto.FlightSergeantIds.Contains(m.CAPID)).ToListAsync(),
            Members = await _context.Members.Where(m => flightDto.MemberIds.Contains(m.CAPID)).ToListAsync(),
        };

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        flightDto.Id = flight.Id;
        return flightDto;
    }

    public async Task<FlightDto> UpdateFlightAsync(int id, FlightDto flightDto)
    {
        var flight = await _context.Flights
            .Include(f => f.FlightSergeants)
            .Include(f => f.Members)
            .FirstOrDefaultAsync(f => f.Id == id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

        flight.Name = flightDto.Name;
        flight.FlightCommanderCAPID = flightDto.FlightCommanderId;

        // Update Flight Sergeants
        var newFlightSergeants = await _context.Members
            .Where(m => flightDto.FlightSergeantIds.Contains(m.CAPID))
            .ToListAsync();

        flight.FlightSergeants.Clear();
        foreach (var sergeant in newFlightSergeants)
        {
            flight.FlightSergeants.Add(sergeant);
        }

        // Update Members
        var newMembers = await _context.Members
            .Where(m => flightDto.MemberIds.Contains(m.CAPID))
            .ToListAsync();

        flight.Members.Clear();
        foreach (var member in newMembers)
        {
            flight.Members.Add(member);
        }

        _context.Flights.Update(flight);
        await _context.SaveChangesAsync();

        return flightDto;
    }

    public async Task DeleteFlightAsync(int id)
    {
        var flight = await _context.Flights
            .Include(f => f.FlightSergeants)
            .Include(f => f.Members)
            .FirstOrDefaultAsync(f => f.Id == id) ?? throw new NotFoundException($"Flight with ID {id} was not found.");

        // Update members to disassociate from the flight
        foreach (var sergeant in flight.FlightSergeants)
        {
            sergeant.FlightSergeantForFlightId = null;
        }

        foreach (var member in flight.Members)
        {
            member.FlightId = null;
        }

        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<int>> GetUnassignedOrCommandersOrSergeantsAsync()
    {
        var capIds = await _context.Members
            .Where(m => !m.FlightId.HasValue ||
                        _context.Flights.Any(f => f.FlightCommanderCAPID == m.CAPID) ||
                        _context.Flights.Any(f => f.FlightSergeants.Any(fs => fs.CAPID == m.CAPID)))
            .Select(m => m.CAPID)
            .ToListAsync();

        return capIds;
    }

}
