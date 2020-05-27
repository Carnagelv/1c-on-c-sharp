using AutoMapper;
using EP.EntityData.Context;
using System;
using System.Linq;

namespace EP.BusinessLogic.Models
{
    public class Mappings
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<UserProfile, UserViewModel>()
                    .ForMember(x => x.CreateDate, y => y.MapFrom(c => c.CreateDate.ToShortDateString()));

                cfg.CreateMap<UserProfile, FriendsModel>()
                    .ForMember(x => x.FullName, y => y.MapFrom(c => $"{c.FirstName} {c.LastName}"))
                    .ForMember(x => x.LastActivyDate, y => y.MapFrom(c => c.LastActivityDate.ToShortDateString()));

                cfg.CreateMap<News, NewsItemViewModel>()
                    .ForMember(x => x.CommentCount, y => y.MapFrom(c => c.NewsCommentaries.Count))
                    .ForMember(x => x.CreateDate, y => y.MapFrom(c => c.CreateDate.ToShortDateString()))
                    .ForMember(x => x.LikeCount, y => y.MapFrom(c => c.NewsLikes.Count))
                    .ForMember(x => x.Commentaries, y => y.MapFrom(c => c.NewsCommentaries.Select(s => new CommentViewModel
                    {
                        CreateDate = s.CreateDate.ToShortDateString(),
                        UserPhoto = s.CreateBy.Photo,
                        Id = s.Id,
                        Text = s.Text,
                        CreateById = s.CreateById,
                        AuthorName = $"{s.CreateBy.FirstName} {s.CreateBy.LastName}"
                    }).ToList()));

                cfg.CreateMap<NewsCommentary, CommentViewModel>()
                    .ForMember(x => x.CreateDate, y => y.MapFrom(c => c.CreateDate.ToShortDateString()))
                    .ForMember(x => x.UserPhoto, y => y.MapFrom(c => c.CreateBy.Photo))
                    .ForMember(x => x.CreateById, y => y.MapFrom(c => c.CreateById))
                    .ForMember(x => x.AuthorName, y => y.MapFrom(c => $"{c.CreateBy.FirstName} {c.CreateBy.LastName}"));

                cfg.CreateMap<CreateTeamModel, Team>()
                    .ForMember(x => x.CreateDate, y => y.UseValue(DateTime.Now))
                    .ForMember(x => x.DisciplineId, y => y.MapFrom(c => c.Discipline))
                    .ForMember(x => x.Name, y => y.MapFrom(c => c.Title))
                    .ForMember(x => x.CreateById, y => y.MapFrom(c => c.OwnerId));

                cfg.CreateMap<Team, TeamViewModel>()
                    .ForMember(x => x.CreatedDate, y => y.MapFrom(c => c.CreateDate.ToShortDateString()))
                    .ForMember(x => x.Discipline, y => y.MapFrom(c => c.DisciplineId))
                    .ForMember(x => x.Name, y => y.MapFrom(c => c.Name))
                    .ForMember(x => x.CreatedBy, y => y.MapFrom(c => $"{c.CreateBy.FirstName} {c.CreateBy.LastName}"))
                    .ForMember(x => x.CreatedById, y => y.MapFrom(c => c.CreateById));

                cfg.CreateMap<Message, DialogView>()
                    .ForMember(x => x.DialogId, y => y.MapFrom(c => c.Id))
                    .ForMember(x => x.IsRead, y => y.MapFrom(c => !c.IsNewMessage))
                    .ForMember(x => x.LastMessage, y => y.MapFrom(c => c.LastMessage))
                    .ForMember(x => x.LastSenderImg, y => y.MapFrom(c => c.LastSender.Photo))
                    .ForMember(x => x.RecipentImg, y => y.MapFrom(c => c.Recipient.Photo))
                    .ForMember(x => x.Recipient, y => y.MapFrom(c => $"{c.Recipient.FirstName} {c.Recipient.LastName}"));

                cfg.CreateMap<Tournament, TournamentsView>()
                    .ForMember(x => x.Date, y => y.MapFrom(c => c.CreateDate.ToShortDateString()))
                    .ForMember(x => x.Discipline, y => y.MapFrom(c => c.DisciplineId));

                cfg.CreateMap<Tournament, ActiveTournamentsView>()
                    .ForMember(x => x.Date, y => y.MapFrom(c => c.TournamentDate.ToShortDateString()))
                    .ForMember(x => x.Discipline, y => y.MapFrom(c => c.DisciplineId))
                    .ForMember(x => x.TeamCount, y => y.MapFrom(c => c.ParticipantsTeams.Count))
                    .ForMember(x => x.IsHorizontal, y => y.MapFrom(c => c.HorizontalPoster))
                    .ForMember(x => x.Fee, y => y.MapFrom(c => $"{c.Fee} €"));

                cfg.CreateMap<Tournament, TournamentView>()
                    .ForMember(x => x.Date, y => y.MapFrom(c => c.TournamentDate.ToShortDateString()))
                    .ForMember(x => x.TeamCount, y => y.MapFrom(c => c.ParticipantsTeams.Count))
                    .ForMember(x => x.Fee, y => y.MapFrom(c => $"{c.Fee} €"))
                    .ForMember(x => x.HasResult, y => y.MapFrom(c => DateTime.Now > c.TournamentDate))
                    .ForMember(x => x.Participants, y => y.MapFrom(c => c.ParticipantsTeams.Select(s => new TournamentParticipant
                    {
                        Id = s.Team.Id,
                        JoinDate = s.JoinDate.ToShortDateString(),
                        LogoUrl = s.Team.LogoUrl,
                        Name = s.Team.Name,
                        Paid = s.Paid,
                        Place = s.Place,
                        Point = s.Point
                    }).ToList()))
                    .ForMember(x => x.Matches, y => y.MapFrom(c => c.Matches.Select(s => new MatchesView
                    {
                        Id = s.Id,
                        FirstTeamLogo = s.FirstTeam.Team.LogoUrl,
                        SecondTeamLogo = s.SecondTeam.Team.LogoUrl,
                        FirstTeamName = s.FirstTeam.Team.Name,
                        SecondTeamName = s.SecondTeam.Team.Name,
                        FirstTeamId = s.FirstTeam.TeamId,
                        SecondTeamId = s.SecondTeam.TeamId,
                        IsWonFirstTeam = s.WonFirstTeam,
                        Date = s.MatchDate.ToString("dd/MM"),
                        IsDraw = s.Draw
                    }).OrderBy(o => o.Id).ToList()));

                cfg.CreateMap<Friend, UserRandomFriend>()
                    .ForMember(x => x.Name, y => y.MapFrom(c => c.With.FirstName))
                    .ForMember(x => x.PhotoUrl, y => y.MapFrom(c => c.With.Photo))
                    .ForMember(x => x.Id, y => y.MapFrom(c => c.With.UserId));

                cfg.CreateMap<Player, PlayerViewModel>()
                    .ForMember(x => x.BirthYear, y => y.MapFrom(c => c.Year))
                    .ForMember(x => x.HasOwner, y => y.MapFrom(c => c.UserId.HasValue))
                    .ForMember(x => x.UserId, y => y.MapFrom(c => c.UserId))
                    .ForMember(x => x.FullName, y => y.MapFrom(c => c.UserId.HasValue ? $"{c.UserProfile.FirstName} {c.UserProfile.LastName}" : string.Empty));

                cfg.CreateMap<Movie, MovieViewModel>();

                cfg.CreateMap<Match, MatchesView>()
                    .ForMember(x => x.Date, y => y.MapFrom(c => c.MatchDate.ToString("dd/MM")))
                    .ForMember(x => x.FirstTeamId, y => y.MapFrom(c => c.FirstTeam.TeamId))
                    .ForMember(x => x.FirstTeamLogo, y => y.MapFrom(c => c.FirstTeam.Team.LogoUrl))
                    .ForMember(x => x.FirstTeamName, y => y.MapFrom(c => c.FirstTeam.Team.Name))
                    .ForMember(x => x.FirstTeamScore, y => y.Ignore())
                    .ForMember(x => x.SecondTeamId, y => y.MapFrom(c => c.SecondTeam.TeamId))
                    .ForMember(x => x.SecondTeamLogo, y => y.MapFrom(c => c.SecondTeam.Team.LogoUrl))
                    .ForMember(x => x.SecondTeamName, y => y.MapFrom(c => c.SecondTeam.Team.Name))
                    .ForMember(x => x.SecondTeamScore, y => y.Ignore())
                    .ForMember(x => x.IsDraw, y => y.MapFrom(c => c.Draw))
                    .ForMember(x => x.IsWonFirstTeam, y => y.MapFrom(c => c.WonFirstTeam));

                cfg.CreateMap<Match, MatchView>()
                    .ForMember(x => x.TournamentName, y => y.MapFrom(c => c.Tournament.Title))
                    .ForMember(x => x.Date, y => y.MapFrom(c => $"{c.Tournament.TournamentDate.ToShortDateString()} {c.Tournament.TournamentDate.ToShortTimeString()}"));
            });

            return config.CreateMapper();
        }
    }
}