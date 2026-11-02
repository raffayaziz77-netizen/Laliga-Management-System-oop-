using System;
using System.Collections.Generic;
using LaLigaApp.DAL;
using LaLigaApp.Models;

namespace LaLigaApp.Logic
{
    public class team_service
    {
        private readonly Team_Repo _teamRepo = new Team_Repo();

        public List<Team> GetAllTeams()
        {
            return _teamRepo.GetAllTeams();
        }

        public void DeleteTeam(int id)
        {
            var teams = _teamRepo.GetAllTeams();
            if (!teams.Exists(t => t.Id == id))
            {
                Console.WriteLine("Team not found.");
                return;
            }
            _teamRepo.DeleteTeam(id);
            Console.WriteLine("Team deleted successfully.");
        }

        public void CreateTeam(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Team name cannot be empty.");
                return;
            }

            Team newTeam = new Team { Name = name, Points = 0 };
            _teamRepo.AddTeam(newTeam);
            Console.WriteLine("Team added successfully!");
        }

        public List<Team> GetStandings()
        {
            var teams = _teamRepo.GetAllTeams();
            teams.Sort((t1, t2) => t2.Points.CompareTo(t1.Points));
            return teams;
        }
        public Team SearchTeam(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _teamRepo.GetTeamByName(name);
        }
        public List<Team> SearchTeamByLetters(string letters)
        {
            var Teams = _teamRepo.GetAllTeams();

            return Teams.FindAll(p =>
            p.Name.IndexOf(letters,
            StringComparison.OrdinalIgnoreCase) >= 0);
        }
        public List<Team> SearchByPoints(int n1,int n2)
        {
            var Teams = _teamRepo.GetAllTeams();

            return Teams.FindAll(t =>
            t.Points > n1 && t.Points < n2);
        }
    }
}