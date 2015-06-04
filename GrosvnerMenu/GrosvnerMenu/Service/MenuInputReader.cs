using GrosvnerMenu.Data;
using GrosvnerMenu.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Service
{
    public interface IMenuInputReader
    {
        string Read(string input, IMenu menu);
    }
    public class MenuInputReader : IMenuInputReader
    {
        const string MORNING = "morning";
        const string NIGHT = "night";
        const string COFFEE = "coffee";
        const string POTATOES = "potato";
        const string ERROR = "error";

        public string Read(string input, IMenu menu)
        {
            if (string.IsNullOrEmpty(input))
                return Print(null, null, menu);

            // Rule #5 Input is not case sensitive
            var formattedInput = input.ToLower();

            // Rule #2a User must enter comma delimited values 
            var inputParts = formattedInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim();
            
            // Rule #1 User must enter "morning" or "night"
            var morning = inputParts.Any(p => p == MORNING);
            var night = inputParts.Any(p => p == NIGHT);
            if ((!morning && !night) || (morning && night))
                return Print(null, null, menu);

            // Rule #2b comma delimited values must have at least one selection
            var selection = inputParts.Where(p => p != MORNING && p != NIGHT).ParseInt();
            return Print(morning, selection, menu);
        }

        /// <summary>
        /// Prints formatted selection based on input
        /// </summary>
        /// <param name="morning">specifies whether input is from the morning menu. Null values will result in a printed error message</param>
        /// <param name="selection">food items identified from user input. Null value will result in a printed error message</param>
        /// <param name="menu">the actual menu</param>
        /// <returns>Appropriate output based on practicum rules</returns>
        private string Print(bool? morning, IEnumerable<int> selection, IMenu menu)
        {
            var result = new List<string>();
            if (!morning.HasValue || selection == null)
                result.Add(ERROR);

            else
            {
                // look for out of range selection
                var max = morning.Value ? menu.MorningMenu.Keys.Max() : menu.NightMenu.Keys.Max();
                var min = morning.Value ? menu.MorningMenu.Keys.Min() : menu.NightMenu.Keys.Min();

                // Rule #3 output must print food in order of menu
                selection = selection.OrderBy(s => s);


                // Perform a "Left Outer Join" using Linq- purpose is to allow items out of range of the input menu. These are paired with an error message
                var lastItem = "";
                var selectedMenu = morning.Value ? menu.MorningMenu : menu.NightMenu;
                var foodItems = selection.Select(s => selectedMenu.ContainsKey(s) ? new KeyValuePair<int, string>(s, selectedMenu[s]) : new KeyValuePair<int, string>(s, ERROR));

                // Check for duplicate items using ordering / a shadow item
                foreach (var item in foodItems)
                {
                    if (!string.IsNullOrEmpty(lastItem)) {
                        
                        // Check duplicate items
                        if (item.Value == lastItem)
                        {
                            // Rule #7 In the morning you can order multiple cups of coffee
                            if (morning.Value) {
                                if (item.Value != COFFEE) {
                                    result.Add(ERROR);
                                    break;
                                }
                            }
                            
                            // Rule #8 At night you can have multiple orders of mashed potatoes
                            else {
                                if (item.Value != POTATOES) {
                                    result.Add(ERROR);
                                    break;
                                }
                            }
                        }

                        // Rule #4 There is no dessert for morning meals
                        if (item.Key == 4 && morning.Value) {
                            result.Add(ERROR);
                            break;
                        }

                        // For invalid inputs - display an error message
                        if (item.Key < min || item.Key > max) {
                            result.Add(ERROR);
                            break;
                        }
                    }
                    result.Add(item.Value);

                    lastItem = item.Value;
                }
            }

            return result.Join(",");
        }
    }
}
