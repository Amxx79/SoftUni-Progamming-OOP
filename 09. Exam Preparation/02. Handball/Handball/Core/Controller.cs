using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> players;
        private IRepository<ITeam> teams;

        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }

        public string NewTeam(string name)
        {
            Team team = new(name);
            if (teams.ExistsModel(name))
            {
                return String.Format(OutputMessages.TeamAlreadyExists, name, "TeamRepository");
            }
            teams.AddModel(team);
            return String.Format(OutputMessages.TeamSuccessfullyAdded, name, "TeamRepository");
        }

        public string NewPlayer(string typeName, string name)
        {
            if (typeName != "Goalkeeper" && typeName != "ForwardWing" && typeName != "CenterBack")
            {
                return String.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }

            else if (players.ExistsModel(name))
            {
                return String.Format(OutputMessages.PlayerIsAlreadyAdded, name, "PlayerRepository", typeName);
            }
            IPlayer player = null;

            if (typeName == "Goalkeeper")
            {
                player = new Goalkeeper(name);
            }
            else if (typeName == "CenterBack")
            {
                player = new CenterBack(name);

            }
            else if (typeName == "ForwardWing")
            {
                player = new ForwardWing(name);
            }
            players.AddModel(player);
            return String.Format(OutputMessages.PlayerAddedSuccessfully, name);
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
            {
                return String.Format(OutputMessages.PlayerNotExisting, playerName, typeof(PlayerRepository).Name);
            }
            else if (!teams.ExistsModel(teamName))
            {
                return String.Format(OutputMessages.TeamNotExisting, teamName, typeof(TeamRepository).Name);
            }

            IPlayer player = players.GetModel(playerName);
            if (player.Team != null)
            {
                return String.Format(OutputMessages.PlayerAlreadySignedContract, playerName, player.Team);
            }
            ITeam team = teams.GetModel(teamName);

            player.JoinTeam(team.Name);
            team.SignContract(player);
            return String.Format(OutputMessages.SignContract, playerName, team.Name);
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firstTeam = teams.GetModel(firstTeamName);
            ITeam secondTeam = teams.GetModel(secondTeamName);

            if (firstTeam.OverallRating > secondTeam.OverallRating)
            {
                firstTeam.Win();
                secondTeam.Lose();
                return String.Format(OutputMessages.GameHasWinner, firstTeam.Name, secondTeam.Name);
            }
            else if (firstTeam.OverallRating < secondTeam.OverallRating)
            {
                firstTeam.Lose();
                secondTeam.Win();
                return String.Format(OutputMessages.GameHasWinner, secondTeam.Name, firstTeam.Name);
            }
            else
            {
                firstTeam.Draw();
                secondTeam.Draw();
                return String.Format(OutputMessages.GameIsDraw, firstTeam.Name, secondTeam.Name);
            }
        }

        public string PlayerStatistics(string teamName)
        {
            ITeam team = teams.GetModel(teamName);
            List<IPlayer> players = new();
            StringBuilder sb = new();
            sb.AppendLine($"***{teamName}***");
            foreach (var player in team.Players)
            {
                players.Add(player);
            }
            players = players.OrderByDescending(p => p.Rating).ThenBy(p => p.Name).ToList();
            foreach (var player in players)
            {
                sb.AppendLine(player.ToString());
            }
            return sb.ToString().Trim();
        }

        public string LeagueStandings()
        {
            StringBuilder tb = new();
            tb.AppendLine("***League Standings***");
            List <ITeam> allTeams = teams.Models.OrderByDescending(t => t.PointsEarned)
                .ThenByDescending(t => t.OverallRating).ThenBy(t => t.Name).ToList();
            foreach (var team in allTeams)
            {
                tb.AppendLine(team.ToString());
            }
            return tb.ToString().Trim();
        }
    }
}
