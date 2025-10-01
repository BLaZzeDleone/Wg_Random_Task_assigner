using System;
using System.Collections.Generic;
using WG_Random_Task_assigner.Object_Folder;

namespace WG_Random_Task_assigner.Tool
{
    public class Task_Assigner_Class
    {
        private readonly WG_Mitglieder _mitglieder = new WG_Mitglieder();
        private readonly BadezimmerTask _badTasks = new BadezimmerTask();
        private readonly KücheTask _kitchenTasks = new KücheTask();
        private readonly ÖffentlicheRäumeTask _publicTasks = new ÖffentlicheRäumeTask();

        private readonly List<string> _bereiche = new List<string> { "Badezimmer", "Küche", "Öffentliche Räume" };

        /// <summary>
        /// Gibt für eine Woche die Zuteilung: Person -> Bereich + alle Tasks dieses Bereichs
        /// </summary>
        public Dictionary<string, (string Bereich, List<string> Tasks)> GetAssignmentsForWeek(int weekOffset)
        {
            var result = new Dictionary<string, (string, List<string>)>();

            // Rotationsbasis: aktueller Wochennummer
            var weekNumber = GetIso8601WeekOfYear(DateTime.Now.AddDays(7 * weekOffset));

            for (int i = 0; i < _mitglieder.mitglieder.Count; i++)
            {
                var person = _mitglieder.mitglieder[i];

                // Bereich im Round-Robin verteilen
                int bereichIndex = (weekNumber + i) % _bereiche.Count;
                string bereich = _bereiche[bereichIndex];

                List<string> tasks = bereich switch
                {
                    "Badezimmer" => _badTasks.tasks,
                    "Küche" => _kitchenTasks.tasks,
                    "Öffentliche Räume" => _publicTasks.tasks,
                    _ => new List<string>()
                };

                result[person] = (bereich, tasks);
            }

            return result;
        }

        public string GetWeekLabel(int weekOffset)
        {
            var today = DateTime.Now.Date.AddDays(7 * weekOffset);
            var weekStart = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var weekEnd = weekStart.AddDays(6);
            return $"{weekStart:dd.MM} - {weekEnd:dd.MM}";
        }

        // Hilfsmethode für ISO Wochennummer
        private static int GetIso8601WeekOfYear(DateTime time)
        {
            var day = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time,
                System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }
    }
}
