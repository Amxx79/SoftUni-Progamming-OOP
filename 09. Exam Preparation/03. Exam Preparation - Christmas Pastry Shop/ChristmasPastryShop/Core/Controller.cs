using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            var booth = new Booth(booths.Models.Count + 1, capacity);
            booths.AddModel(booth);

            return $"Added booth number {booth.BoothId} with capacity {capacity} in the pastry shop!";
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            string[] validTypes = { "Hibernation", "MulledWine" };
            if (cocktailTypeName != validTypes[0] && cocktailTypeName != validTypes[1])
            {
                return $"Cocktail type {cocktailTypeName} is not supported in our application!";
            }
            else if (size != "Large" && size != "Middle" && size != "Small")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            var cocktail = booth
                .CocktailMenu.Models.FirstOrDefault(c => c.Size == size && c.Name == cocktailName);
            if (cocktail != null)
            {
                return $"{size} {cocktailName} is already added in the pastry shop!";
            }
            else
            {
                if (cocktailTypeName == "Hibernation")
                {
                    cocktail = new Hibernation(cocktailName, size);
                }
                else if (cocktailTypeName == "MulledWine")
                {
                    cocktail = new MulledWine(cocktailName, size);
                }
                booth.CocktailMenu.AddModel(cocktail);
                return $"{size} {cocktailName} {cocktailTypeName} added to the pastry shop!";
                
            }
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != "Stolen" && delicacyTypeName != "Gingerbread")
            {
                return $"Delicacy type {delicacyTypeName} is not supported in our application!";
            }

            var booth = booths.Models.First(b => b.BoothId == boothId);
            var delicacy = booth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == delicacyName);

            if (delicacy != null)
            {
                return $"{delicacyName} is already added in the pastry shop!";
            }

            if (delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            else if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }

            booth.DelicacyMenu.AddModel(delicacy);

            return $"{delicacyTypeName} {delicacyName} added to the pastry shop!";
        }

        public string BoothReport(int boothId)
        {
            var booth = booths.Models.First(b => b.BoothId == boothId);

            return booth.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            double bill = booth.CurrentBill;
            booth.Charge();
            booth.ChangeStatus();
            var sb = new StringBuilder();
            sb.AppendLine($"Bill {bill:f2} lv");
            sb.AppendLine($"Booth {boothId} is now available!");
            return sb.ToString().Trim();
        }

        public string ReserveBooth(int countOfPeople)
        {
            var booth = booths.Models.Where(b => !b.IsReserved && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity).ThenByDescending(b => b.BoothId).FirstOrDefault();
            if (booth == null)
            {
                return $"No available booth for {countOfPeople} people!";
            }
            else
            {
                booth.ChangeStatus();
                return $"Booth {booth.BoothId} has been reserved for {countOfPeople} people!";
            }
        }

        public string TryOrder(int boothId, string order)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            string[] currentOrder = order.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var itemTypeName = currentOrder[0];
            var itemName = currentOrder[1];
            var count = int.Parse(currentOrder[2]);
            var size = string.Empty;

            if (itemTypeName == "Hibernation" || itemTypeName == "MulledWine")
            {
                size = currentOrder[3];
            }

            if (currentOrder[0] != "Hibernation" 
                && currentOrder[0] != "MulledWine" 
                && currentOrder[0] != "Gingerbread" 
                && currentOrder[0] != "Stolen")
            {
                return $"{currentOrder[0]} is not recognized type!";
            }
            if (currentOrder[0] == "Hibernation" || currentOrder[0] == "MulledWine")
            {
                if (!booth.CocktailMenu.Models.Any(c => c.Name == itemName))
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }

                var cocktail = booth.CocktailMenu.Models
                    .FirstOrDefault(c => c.Name == itemName && c.Size == size);
                if (cocktail == null)
                {
                    return $"There is no {size} {itemName} available!";
                }
                else
                {
                    booth.UpdateCurrentBill(cocktail.Price * count);
                    return $"Booth {boothId} ordered {count} {itemName}!";
                }
            }

            else
            {
                    var delicacy = booth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == itemName);
                    if (delicacy == null)
                    {
                        return $"There is no {itemTypeName} {itemName} available!";
                    }
                    else
                    {
                        booth.UpdateCurrentBill(delicacy.Price * count);
                        return $"Booth {boothId} ordered {count} {itemName}!";
                    }
            }
        }
    }
}
