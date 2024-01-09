using Todoist.Views;
using Todoist.Model;
using Todoist.Entities;
using Todoist.Consts;

namespace Todoist.Controllers
{
    internal class ControllerConsole
    {
        private readonly ModelConsole _modelConsole;
        private readonly ViewConsole _viewConsole;

        internal ControllerConsole(ModelConsole modelConsole, ViewConsole viewConsole)
        {
            _modelConsole = modelConsole;
            _viewConsole = viewConsole;
        }
        
        internal void AddGoal()
        {
            List<Category> categories = _modelConsole.GetCategories();
            string[] statuses = _modelConsole.GetStatuses();
            int categoryId;
            string choiceCategory;
            string choiceStatus;
            string title;
            string description;
            bool isValid;

            _viewConsole.Display(AppConsts.Suggestion.Enter.NewTitle);
            do
            {
                title = _viewConsole.GetInput();
                isValid = CheckLengthTitleOrDescription(title, AppConsts.Common.NumberOf.MaximumCharactersForTitleAndDescription);
                if (!isValid) _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
            }
            while (!isValid);

            _viewConsole.Display(AppConsts.Suggestion.Enter.NewDescription);
            do
            {
                description = _viewConsole.GetInput();
                isValid = CheckLengthTitleOrDescription(title, AppConsts.Common.NumberOf.MaximumCharactersForTitleAndDescription);
                if (!isValid) _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
            }
            while (!isValid);

            _viewConsole.Display(AppConsts.Suggestion.Select.CategoryOfGoal);
            _viewConsole.OutputCategoryNames(categories);
            choiceCategory = CheckValidation(_viewConsole.GetInput(), categories.Count);
            categoryId = categories[Convert.ToInt32(choiceCategory) - 1].Id;

            _viewConsole.Display(AppConsts.Suggestion.Select.StatusOfGoal);
            _viewConsole.OutputOfAvaliableStatuses(statuses);
            choiceStatus = CheckValidation(_viewConsole.GetInput(), statuses.Length);
            string status = _modelConsole.SearchEnumByIndex(choiceStatus);

            _modelConsole.Add(title, description, status, categoryId);
            _viewConsole.Display(AppConsts.Common.TaskAdded);
        }

        internal void ViewGoalList()
        {
            List<Category> categories = _modelConsole.GetCategories();
            List<Goal> goals = _modelConsole.GetGoals();
            _viewConsole.OutputCategories(categories, goals);
        }

        internal void FindGoal()
        {
            List<Goal> goals = _modelConsole.GetGoals();
            List<Goal> results;
            string searchWord;
            bool isValid;

            _viewConsole.Display(AppConsts.Suggestion.Enter.WordForSearch);
            do
            {
                searchWord = _viewConsole.GetInput();
                isValid = CheckLengthTitleOrDescription(searchWord, AppConsts.Common.NumberOf.MaximumCharactersForTitleAndDescription);
                if (!isValid) _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
            }
            while (!isValid);

            results = _modelConsole.SearchElementsByTitleAndDescription(goals, searchWord);
            if (results.Count == 0)
                _viewConsole.Display(AppConsts.Suggestion.Enter.NotFound);
            else
                _viewConsole.OutputGoals(results);
        }

        internal void UpdateGoal()
        {
            List<Goal> goals = _modelConsole.GetGoals();
            List<string> updatedProperties = new List<string>();
            string choice;

            _viewConsole.Display(AppConsts.Suggestion.Select.Goal + "\n");
            _viewConsole.OutputGoals(goals);
            choice = CheckValidation(_viewConsole.GetInput(), goals.Count);
            var goalForUpdate = _modelConsole.SearchElementByIndex(goals, Convert.ToInt32(choice));
            _viewConsole.Display(goalForUpdate);

            string titleOfGoal = GetNewTitleOfGoal();
            updatedProperties.Add(titleOfGoal);

            string descriptionOfGoal = GetNewDescriptionOfGoal();
            updatedProperties.Add(descriptionOfGoal);

            string categoryOfGoal = GetNewIDCategoryOfGoal();
            updatedProperties.Add(categoryOfGoal);

            string statusOfGoal = GetNewStatusOfGoal();
            updatedProperties.Add(statusOfGoal);

            _modelConsole.Update(goalForUpdate, updatedProperties);
            _viewConsole.Display(AppConsts.Common.TaskChanged);
        }

        internal void DeleteGoal()
        {
            List<Goal> Goals = _modelConsole.GetGoals();
            string choice;

            _viewConsole.Display(AppConsts.Suggestion.Select.Goal);
            _viewConsole.OutputGoals(Goals);

            choice = CheckValidation(_viewConsole.GetInput(), Goals.Count);

            var searchedElementGoal = _modelConsole.SearchElementByIndex(Goals, Convert.ToInt32(choice));
            _viewConsole.Display(searchedElementGoal);

            _viewConsole.Display(AppConsts.Question.ForDelete.Confirmation + AppConsts.Common.Menu.YesNoSelectable);
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);

            if (choice == "1")
            {
                _modelConsole.Delete(searchedElementGoal);
                _viewConsole.Display(AppConsts.Common.TaskDelete);
            }
        }


        internal string GetNewTitleOfGoal()
        {
            string choice;
            string newTitle;

            _viewConsole.Display($"{AppConsts.Question.ForUpdate.Title}\n{AppConsts.Common.Menu.YesNoSelectable}");
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
            if (choice == "1")
            {
                _viewConsole.Display(AppConsts.Suggestion.Enter.NewTitle);
                newTitle = _viewConsole.GetInput();
                return newTitle;
            }
            else
                return _viewConsole.GetEmpty();
        }

        internal string GetNewDescriptionOfGoal()
        {
            string choice;
            string newDescription;

            _viewConsole.Display($"{AppConsts.Question.ForUpdate.Description}\n{AppConsts.Common.Menu.YesNoSelectable}");
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
            if (choice == "1")
            {
                _viewConsole.Display(AppConsts.Suggestion.Enter.NewDescription);
                newDescription = _viewConsole.GetInput();
                return newDescription;
            }
            else
                return _viewConsole.GetEmpty();
        }

        internal string GetNewIDCategoryOfGoal()
        {
            List<Category> categories = new List<Category>();
            string choice;
            _viewConsole.Display($"{AppConsts.Question.ForUpdate.Category}\n{AppConsts.Common.Menu.YesNoSelectable}");
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
            if (choice == "1")
            {
                _viewConsole.Display(AppConsts.Suggestion.Select.CategoryOfGoal);
                categories = _modelConsole.GetCategories();
                _viewConsole.OutputCategoryNames(categories);
                return CheckValidation(_viewConsole.GetInput(), categories.Count);
            }
            else
                return _viewConsole.GetEmpty();
        }

        internal string GetNewStatusOfGoal()
        {
            string[] statuses;
            string choice;

            _viewConsole.Display($"{AppConsts.Question.ForUpdate.Status}\n{AppConsts.Common.Menu.YesNoSelectable}");
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
            if (choice == "1")
            {
                _viewConsole.Display(AppConsts.Suggestion.Select.StatusOfGoal);
                statuses = _modelConsole.GetStatuses();
                _viewConsole.OutputOfAvaliableStatuses(statuses);
                choice = CheckValidation(_viewConsole.GetInput(), statuses.Length);
                return _modelConsole.SearchEnumByIndex(choice);
            }
            else
                return _viewConsole.GetEmpty();
        }


        internal bool CheckLengthTitleOrDescription(string titleOrDescription, int maximumCharacters)
        {
            return titleOrDescription.Length <= maximumCharacters;
        }

        private bool Validation(string testedItem, int numberOfElementsMenu)
        {
            return !string.IsNullOrEmpty(testedItem)
                && uint.TryParse(testedItem, out uint res)
                && Convert.ToInt32(testedItem) != 0
                && Convert.ToInt32(testedItem) <= numberOfElementsMenu;
        }

        private string CheckValidation(string choice, int numberOfElementsMenu)
        {
            while (!Validation(choice, numberOfElementsMenu))
            {
                _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
                choice = _viewConsole.GetInput();
            }
            return choice;
        }

        internal void CheckAndImplementTheStartMenuItem(string choice)
        {
            if (choice == "1")
                AddGoal();

            else if (choice == "2")
                ViewGoalList();

            else if (choice == "3")
                FindGoal();

            else if (choice == "4")
                UpdateGoal();

            else if (choice == "5")
                DeleteGoal();

            else if (choice == "6")
                Environment.Exit(0);
        }

        internal void StartApplication()
        {
            string choice;
            _viewConsole.Display(AppConsts.Common.Menu.Start + AppConsts.Common.Menu.StartItemSelectable);
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.StartItems);
            CheckAndImplementTheStartMenuItem(choice);
        }
    }
}
