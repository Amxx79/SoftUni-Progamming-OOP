using Raiding.Core.Interfaces;
using Raiding.Factories.Interfaces;
using Raiding.IO;
using Raiding.IO.Interfaces;
using Raiding.Models;
using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Core
{
    internal class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IHeroFactory heroFactory;

        private readonly ICollection<IHero> allHeroes;

        public Engine(IReader reader, IWriter writer, IHeroFactory herofactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.heroFactory = herofactory;

            allHeroes = new List<IHero>();
        }
        public void Run()
        {
            int heroes = int.Parse(reader.ReadLine());

            for (int i = 0; i < heroes; i++)
            {
                try
                {
                    string name = reader.ReadLine();
                    string cast = reader.ReadLine();

                    if (cast == "Paladin")
                    {
                        allHeroes.Add(heroFactory.Create(cast, name));
                    }
                    else if (cast == "Druid")
                    {
                        allHeroes.Add(heroFactory.Create(cast, name));
                    }
                    else if (cast == "Rogue")
                    {
                        allHeroes.Add(heroFactory.Create(cast, name));
                    }
                    else if (cast == "Warrior")
                    {
                        allHeroes.Add(heroFactory.Create(cast, name));
                    }
                    else
                    {
                        throw new ArgumentException("Invalid hero!");
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            int bossPower = int.Parse(reader.ReadLine());

            foreach(var hero in allHeroes)
            {
                writer.WriteLine(hero.CastAbility());
            }


            if (allHeroes.Sum(h => h.Power) >= bossPower)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }
    }
}
