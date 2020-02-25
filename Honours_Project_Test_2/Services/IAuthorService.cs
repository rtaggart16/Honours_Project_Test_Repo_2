using Honours_Project_Test_2.Models.ViewModels.Author;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkAPI.Data;

namespace Honours_Project_Test_2.Services
{
    public interface IAuthorService
    {
        List<Basic_Author_Info> Get_All_Authors();
    }

    public class AuthorService : IAuthorService
    {
        private readonly AppDB _db;

        public AuthorService(AppDB db)
        {
            _db = db;
        }

        public List<Basic_Author_Info> Get_All_Authors()
        {
            List<Basic_Author_Info> authorInfos = new List<Basic_Author_Info>();

            var authors = _db.Authors.Include(x => x.User).ToList();

            if (authors.Count() >= 1)
            {
                foreach (var author in authors)
                {
                    authorInfos.Add(new Basic_Author_Info()
                    {
                        ID = author.ID,
                        First_Name = author.User.First_Name,
                        Last_Name = author.User.Last_Name,
                        Photo = author.Profile_Photo
                    });
                }
            }

            return authorInfos;
        }
    }
}

