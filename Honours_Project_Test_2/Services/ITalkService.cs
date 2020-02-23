using Honours_Project_Test_2.Models;
using Honours_Project_Test_2.Models.ViewModels.Talk;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkAPI.Data;

namespace Honours_Project_Test_2.Services
{
    public interface ITalkService
    {
        List<Basic_Talk_Info> Get_All_Talks();

        Interest_Result Log_Interest(Interest_Request request, bool overwrite);

        bool Unmark_Interest(Interest_Request request);

        List<Basic_Talk_Info> Get_All_Talks_For_User(string userID, string option, string optionID);

        bool Rate_Talk(User_Rating_Request request);
    }

    public class TalkService : ITalkService
    {
        private readonly AppDB _db;

        public TalkService(AppDB db)
        {
            _db = db;
        }

        public List<Basic_Talk_Info> Get_All_Talks()
        {
            var talks = _db.Talks
                .Include(x => x.Ratings)
                .Include(x => x.Author)
                .ThenInclude(x => x.User)
                .Include(x => x.Sessions)
                .ThenInclude(x => x.Session)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .ToList();

            List<Basic_Talk_Info> talkInfo = new List<Basic_Talk_Info>();

            if (talks.Count() > 0)
            {
                foreach (var talk in talks)
                {

                    talkInfo.Add(new Basic_Talk_Info()
                    {
                        ID = talk.ID,
                        Name = talk.Title,
                        Description = talk.Description,
                        Interest_Count = 0,
                        Ratings = talk.Ratings.Select(x => x.Rating).ToList(),
                        Session = talk.Sessions.FirstOrDefault().Session,
                        Tags = talk.Tags.Select(x => x.Tag.Name).ToList(),
                        Start = talk.Start,
                        End = talk.End,
                        Author = new Models.ViewModels.Author.Basic_Author_Info()
                        {
                            First_Name = talk.Author.User.First_Name,
                            Last_Name = talk.Author.User.Last_Name,
                            Username = talk.Author.User.UserName,
                            Photo = talk.Author.Profile_Photo
                        }
                    });
                }
            }

            talkInfo = talkInfo.OrderBy(x => x.Start).ToList();
            return talkInfo;
        }

        public List<Basic_Talk_Info> Get_All_Talks_For_User(string userID, string option, string optionID)
        {
            var userInterests = _db.User_Talk_Interests.Where(x => x.User_ID == userID);
            var userRatings = _db.User_Talk_Ratings.Where(x => x.User_ID == userID);

            var dbTalks = _db.Talks
                .Include(x => x.Ratings)
                .Include(x => x.Author)
                .ThenInclude(x => x.User)
                .Include(x => x.Sessions)
                .ThenInclude(x => x.Session)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Ratings)
                .ToList();

            var talks = new List<Talk>();

            if (option != "none" && optionID != "none")
            {
                switch (option)
                {
                    case "tag":
                        talks = dbTalks.Where(x => x.Tags.Select(c => c.Tag_ID).Contains(optionID)).ToList();
                        break;

                    case "session":
                        talks = dbTalks.Where(x => x.Sessions.FirstOrDefault().Session_ID == optionID).ToList();
                        break;

                    case "author":
                        talks = dbTalks.Where(x => x.Author_ID == optionID).ToList();
                        break;
                }
            }
            else
            {
                talks = dbTalks;
            }

            List<Basic_Talk_Info> talkInfo = new List<Basic_Talk_Info>();

            if (talks.Count() > 0)
            {
                foreach (var talk in talks)
                {
                    bool userInterested = false;
                    bool userRated = false;

                    decimal userRating = 0;

                    if (userInterests.Select(x => x.Talk_ID).Contains(talk.ID))
                    {
                        userInterested = true;
                    }

                    if (userRatings.FirstOrDefault(x => x.User_ID == userID && x.Talk_ID == talk.ID) != null)
                    {
                        userRated = true;
                        userRating = userRatings.FirstOrDefault(x => x.User_ID == userID && x.Talk_ID == talk.ID).Rating;
                    }

                    talkInfo.Add(new Basic_Talk_Info()
                    {
                        ID = talk.ID,
                        Name = talk.Title,
                        Description = talk.Description,
                        Interest_Count = 0,
                        Ratings = talk.Ratings.Select(x => x.Rating).ToList(),
                        Session = talk.Sessions.FirstOrDefault().Session,
                        Tags = talk.Tags.Select(x => x.Tag.Name).ToList(),
                        Start = talk.Start,
                        End = talk.End,
                        Author = new Models.ViewModels.Author.Basic_Author_Info()
                        {
                            First_Name = talk.Author.User.First_Name,
                            Last_Name = talk.Author.User.Last_Name,
                            Username = talk.Author.User.UserName,
                            Photo = talk.Author.Profile_Photo
                        },
                        User_Interested = userInterested,
                        User_Rated = userRated,
                        User_Rating = userRating
                    });
                }
            }

            talkInfo = talkInfo.OrderBy(x => x.Start).ToList();
            return talkInfo;
        }

        public Interest_Result Log_Interest(Interest_Request request, bool overwrite)
        {
            var existingTalk = _db.Talks
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Author)
                .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.ID == request.Talk_ID);

            if (overwrite)
            {
                var ticketInterest = _db.User_Talk_Interests.FirstOrDefault(x => x.Talk_ID == request.Talk_ID && x.User_ID == request.User_ID);

                ticketInterest.Talk_ID = request.Overwrite_ID;

                _db.User_Talk_Interests.Update(ticketInterest);
                _db.SaveChanges();

                return new Interest_Result()
                {
                    Success = true,
                    Interest_Talk = null,
                    Overwrite_Talk = null
                };
            }
            else
            {
                var existingInterests = _db.User_Talk_Interests.Where(x => x.User_ID == request.User_ID);

                if (existingInterests.Count() == 0)
                {
                    User_Talk_Interest userInterest = new User_Talk_Interest()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Talk_ID = request.Talk_ID,
                        User_ID = request.User_ID
                    };

                    _db.User_Talk_Interests.Add(userInterest);
                    _db.SaveChanges();

                    return new Interest_Result()
                    {
                        Success = true,
                        Interest_Talk = null,
                        Overwrite_Talk = null
                    };
                }
                else
                {
                    var conflictingSessions = existingInterests.Where(x => x.Talk.Start >= existingTalk.Start && x.Talk.End <= existingTalk.End).Count();

                    if (conflictingSessions > 0)
                    {
                        var conflictingTalk = existingInterests
                            .Include(x => x.Talk)
                            .ThenInclude(x => x.Tags)
                    .Include(x => x.Talk)
                    .ThenInclude(x => x.Author)
                    .ThenInclude(x => x.User)
                    .Where(x => x.Talk.Start >= existingTalk.Start && x.Talk.End <= existingTalk.End && x.Talk_ID != existingTalk.ID).FirstOrDefault().Talk;

                        return new Interest_Result()
                        {
                            Success = false,
                            Interest_Talk = new Basic_Talk_Info()
                            {
                                ID = existingTalk.ID,
                                Name = existingTalk.Title,
                                Author = new Models.ViewModels.Author.Basic_Author_Info()
                                {
                                    First_Name = existingTalk.Author.User.First_Name,
                                    Last_Name = existingTalk.Author.User.Last_Name
                                },
                                Description = existingTalk.Description,
                                Start = existingTalk.Start,
                                End = existingTalk.End,
                                Tags = existingTalk.Tags.Select(x => x.Tag.Name).ToList()
                            },
                            Overwrite_Talk = new Basic_Talk_Info()
                            {
                                ID = conflictingTalk.ID,
                                Name = conflictingTalk.Title,
                                Author = new Models.ViewModels.Author.Basic_Author_Info()
                                {
                                    First_Name = conflictingTalk.Author.User.First_Name,
                                    Last_Name = conflictingTalk.Author.User.Last_Name
                                },
                                Description = conflictingTalk.Description,
                                Start = conflictingTalk.Start,
                                End = conflictingTalk.End
                            }
                        };
                    }
                    else
                    {
                        User_Talk_Interest userInterest = new User_Talk_Interest()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Talk_ID = request.Talk_ID,
                            User_ID = request.User_ID
                        };

                        _db.User_Talk_Interests.Add(userInterest);
                        _db.SaveChanges();

                        return new Interest_Result()
                        {
                            Success = true,
                            Interest_Talk = null,
                            Overwrite_Talk = null
                        };
                    }
                }
            }

        }

        public bool Unmark_Interest(Interest_Request request)
        {
            var userInterest = _db.User_Talk_Interests.FirstOrDefault(x => x.User_ID == request.User_ID && x.Talk_ID == request.Talk_ID);

            if (userInterest != null)
            {
                _db.User_Talk_Interests.Remove(userInterest);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        public bool Rate_Talk(User_Rating_Request request)
        {
            var existingRating = _db.User_Talk_Ratings.FirstOrDefault(x => x.Talk_ID == request.Talk_ID && x.User_ID == request.User_ID);

            if (existingRating == null)
            {
                User_Talk_Rating newRating = new User_Talk_Rating()
                {
                    ID = Guid.NewGuid().ToString(),
                    Talk_ID = request.Talk_ID,
                    User_ID = request.User_ID,
                    Rating = request.Rating
                };

                _db.User_Talk_Ratings.Add(newRating);
            }
            else
            {
                existingRating.Rating = request.Rating;

                _db.User_Talk_Ratings.Update(existingRating);
            }

            _db.SaveChanges();

            return true;
        }
    }
}
