using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stories.API.Application.Models.Requests;
using Stories.API.Controllers;
using Stories.API.Data;
using Stories.API.Data.Models;
using Stories.API.Services;
using Stories.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.UnitTest
{
    public class VoteServiceTest 
    {
        //private readonly StoriesContext _context;

        //public VoteServiceTest()
        //{
        //    DbContextOptions<StoriesContext> options = new DbContextOptionsBuilder<StoriesContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;

        //    _context = new StoriesContext(options);
        //}      
    }
}
