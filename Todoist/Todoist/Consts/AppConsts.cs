namespace Todoist.Consts
{
    internal  class AppConsts
    {
        internal  class Common
        {
            internal  class Menu
            {
                public const string Start = "\tMenu\n 1. Create a task\n 2. View task list\n 3. Find the task\n 4. Change the task\n 5. Delete the task\n 6. Exit\n";
                public const string StartItemSelectable = " Make a selection by entering a number...\n";
                public const string YesNoSelectable = " 1. Yes\n 2. No\n";
            }

            internal  class NumberOf
            {
                internal const int StartItems = 6;
                internal const int YesOrNoItems = 2;
                internal const int ElementsForUpdate = 4;
                internal const int MaximumCharactersForTitleAndDescription = 30;
            }
        }
        internal  class Suggestion
        {
            internal  class Enter
            {
                public const string NewTitle = " Enter the TITLE of your task (maximum 30):\n";
                public const string NewDescription = " Enter the DESCRIPTION of your task:\n";
                public const string WordForSearch = " Enter a word to search by TITLE or DESCRIPTION:";
                public const string NotFound = "\n Nothing was found for your request...\n";
                public const string ValidValue = "Please, enter a valid value!:)\n";
            }
            internal  class Select
            {
                public const string Goal = " Select a number of task:\n";
                public const string CategoryOfGoal = " Select the CATEGORY to which you task will belong:";
                public const string StatusOfGoal = " Select the STATUS to which you task will belong:\n";
            }
        }

        internal  class Question
        {
            internal  class ForUpdate
            {
                public const string Title = " Do you want to update TITLE of chosen task?";
                public const string Description = " Do you want to update DESCRIPTION of chosen task?";
                public const string Category = " Do you want to update CATEGORY of chosen task?";
                public const string Status = " Do you want to update STATUS of chosen task?\n";
            }

            internal  class ForDelete
            {
                public const string Confirmation = " Are you sure you want to delete the selected item?\n";
            }
        }
    }
}
