namespace Todoist.Core.Consts;

public class AppConsts
{
    public class Common
    {
        public class Menu
        {
            public const string Start = "\tMenu\n 1. Create a task\n 2. View task list\n 3. Find the task\n 4. Change the task\n 5. Delete the task\n 6. Exit\n";
            public const string StartItemSelectable = " Make a selection by entering a number...\n";
            public const string YesNoSelectable = " 1. Yes\n 2. No\n";
        }

        public class NumberOf
        {
            public const int StartItems = 6;
            public const int YesOrNoItems = 2;
            public const int ElementsForUpdate = 4;
            public const int MaximumCharactersForTitle = 30;
            public const int MaximumCharactersForDescription = 100;
        }

        public const string TaskAdded = " \nTask successfully added!\n";
        public const string TaskChanged = " \nTask successfully changed!\n";
        public const string TaskDelete = " \nTask successfully deleted!\n";
    }
    public class Suggestion
    {
        public class Enter
        {
            public const string NewTitle = " Enter the TITLE of your task (maximum of 30 characters):\n";
            public const string NewDescription = " Enter the DESCRIPTION of your task (maximum of 100 characters):\n";
            public const string WordForSearch = " Enter a word to search by TITLE or DESCRIPTION:";
            public const string NotFound = "\n Nothing was found for your request...\n";
            public const string ValidValue = "Please, enter a valid value!:)\n";
        }
        public  class Select
        {
            public const string Goal = " Select a number of task:\n";
            public const string CategoryOfGoal = " Select the CATEGORY to which you task will belong:";
            public const string StatusOfGoal = " Select the STATUS to which you task will belong:\n";
        }
    }

    public class Question
    {
        public class ForUpdate
        {
            public const string Title = " Do you want to update TITLE of chosen task?";
            public const string Description = " Do you want to update DESCRIPTION of chosen task?";
            public const string Category = " Do you want to update CATEGORY of chosen task?";
            public const string Status = " Do you want to update STATUS of chosen task?";
        }

        public class ForDelete
        {
            public const string Confirmation = " Are you sure you want to delete the selected item?\n";
        }
    }
}
