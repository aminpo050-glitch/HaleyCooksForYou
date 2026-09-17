using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace HaleyCooksForYou
{
    public class ModEntry : Mod
    {
        private bool _gaveBreakfast;
        private bool _gaveLunch;
        private bool _gaveDinner;

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.DayStarted += OnDayStarted;
            helper.Events.Display.MenuChanged += OnMenuChanged;
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            _gaveBreakfast = false;
            _gaveLunch = false;
            _gaveDinner = false;
        }

        private void OnMenuChanged(object sender, MenuChangedEventArgs e)
        {
            if (e.NewMenu is DialogueBox && Game1.currentSpeaker?.Name == "Haley")
            {
                if (Game1.player.spouse != "Haley")
                    return;

                int time = Game1.timeOfDay;

                if (time >= 600 && time <= 700 && !_gaveBreakfast)
                {
                    _gaveBreakfast = true;
                    GiveMeal("(O)194", "here's your breakfast honey");
                }
                else if (time >= 1300 && time <= 1400 && !_gaveLunch)
                {
                    _gaveLunch = true;
                    GiveMeal("(O)206", "I packed lunch for you");
                }
                else if (time >= 2000 && time <= 2200 && !_gaveDinner)
                {
                    _gaveDinner = true;
                    GiveMeal("(O)206", "I made dinner today");
                }
            }
        }

        private void GiveMeal(string itemId, string message)
        {
            Item meal = ItemRegistry.Create(itemId);
            Game1.player.addItemByMenuIfNecessary(meal);
            Game1.drawDialogue(Game1.currentSpeaker, message);
        }
    }
}
