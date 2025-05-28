using System;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Services;

public class GroupService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Group> AddGroup(Group group ){
        _context.Add(group);
        await _context.SaveChangesAsync();
        return group;
    }

    public async Task<Group> GetGroup(int id){
        var group = _context.Groups.FirstOrDefault(g => g.Id == id);

        return group;
    }

    public async Task<Group[]> GetGroups(){
        var groups = _context.Groups.ToArray();

        return groups;
    }
}
