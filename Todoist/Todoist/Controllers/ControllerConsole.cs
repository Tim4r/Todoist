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

        internal async Task AddGoalAsync()
        {
            string newTitle;
            string newDescription;
            int IdOfSelectedCategory;
            string selectedStatus;
            
            newTitle = CreateAndCheckTitleOfGoal();
            newDescription = CreateAndCheckDescriptionOfGoal();
            IdOfSelectedCategory = SelectAndFindIdOfCategory(_modelConsole.GetCategoriesAsync());
            selectedStatus = SelectAndCheckStatusOfCategory(_modelConsole.GetStatuses());

            await _modelConsole.AddAsync(newTitle, newDescription, selectedStatus, IdOfSelectedCategory);
            _viewConsole.Display(AppConsts.Common.TaskAdded);
        }

        internal void ViewGoalList()
        {
            Task<List<Category>> categories = _modelConsole.GetCategoriesAsync();
            Task<List<Goal>> goals = _modelConsole.GetGoalsAsync();
            _viewConsole.OutputCategories(categories, goals);
        }
        
        internal async Task FindGoalAsync()
        {
            List<Goal> goals = await _modelConsole.GetGoalsAsync();
            List<Goal> results;
            string searchWord;

            _viewConsole.Display(AppConsts.Suggestion.Enter.WordForSearch);
            do
            {
                searchWord = _viewConsole.GetInput();
                if (!(searchWord.Length <= AppConsts.Common.NumberOf.MaximumCharactersForDescription)) _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
            }
            while (!(searchWord.Length <= AppConsts.Common.NumberOf.MaximumCharactersForDescription));

            results = _modelConsole.SearchElementsByTitleAndDescription(goals, searchWord);
            if (results.Count == 0)
                _viewConsole.Display(AppConsts.Suggestion.Enter.NotFound);
            else
                _viewConsole.OutputGoals(results);
        }

        internal async Task UpdateGoalAsync()
        {
            Task<List<Goal>> goals = _modelConsole.GetGoalsAsync();
            List<string> updatedProperties = new List<string>();
            string choice;

            _viewConsole.Display(AppConsts.Suggestion.Select.Goal + "\n");
            _viewConsole.OutputGoals(goals);
            choice = CheckValidation(_viewConsole.GetInput(), goals.Result.Count);
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

            await _modelConsole.UpdateAsync(goalForUpdate, updatedProperties);
            _viewConsole.Display(AppConsts.Common.TaskChanged);
        }

        internal async Task DeleteGoalAsync()
        {
            Task<List<Goal>> Goals = _modelConsole.GetGoalsAsync();
            string choice;

            _viewConsole.Display(AppConsts.Suggestion.Select.Goal);
            _viewConsole.OutputGoals(Goals);

            choice = CheckValidation(_viewConsole.GetInput(), Goals.Result.Count);

            var searchedElementGoal = _modelConsole.SearchElementByIndex(Goals, Convert.ToInt32(choice));
            _viewConsole.Display(searchedElementGoal);

            _viewConsole.Display(AppConsts.Question.ForDelete.Confirmation + AppConsts.Common.Menu.YesNoSelectable);
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);

            if (choice == "1")
            {
                await _modelConsole.DeleteAsync(searchedElementGoal);
                _viewConsole.Display(AppConsts.Common.TaskDelete);
            }
        }

        internal string CreateAndCheckTitleOfGoal()
        {
            string newTitle;
            _viewConsole.Display(AppConsts.Suggestion.Enter.NewTitle);
            do
            {
                newTitle = _viewConsole.GetInput();
                if (!(newTitle.Length <= AppConsts.Common.NumberOf.MaximumCharactersForTitle)) _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
            }
            while (!(newTitle.Length <= AppConsts.Common.NumberOf.MaximumCharactersForTitle));
            return newTitle;
        }

        internal string CreateAndCheckDescriptionOfGoal()
        {
            string newDescription;
            _viewConsole.Display(AppConsts.Suggestion.Enter.NewDescription);
            do
            {
                newDescription = _viewConsole.GetInput();
                if (!(newDescription.Length <= AppConsts.Common.NumberOf.MaximumCharactersForDescription)) _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
            }
            while (!(newDescription.Length <= AppConsts.Common.NumberOf.MaximumCharactersForDescription));
            return newDescription;
        }

        internal int SelectAndFindIdOfCategory(Task<List<Category>> categories)
        {
            string selectedCategory;
            _viewConsole.Display(AppConsts.Suggestion.Select.CategoryOfGoal);
            _viewConsole.OutputCategoryNames(categories);
            selectedCategory = CheckValidation(_viewConsole.GetInput(), categories.Result.Count);
            return categories.Result[Convert.ToInt32(selectedCategory) - 1].Id;
        }

        internal string SelectAndCheckStatusOfCategory(string[] statuses)
        {
            string selectedStatus;
            _viewConsole.Display(AppConsts.Suggestion.Select.StatusOfGoal);
            _viewConsole.OutputOfAvaliableStatuses(statuses);
            selectedStatus = CheckValidation(_viewConsole.GetInput(), statuses.Length);
            return _modelConsole.SearchEnumByIndex(selectedStatus);
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
            Task<List<Category>> categories;
            string choice;
            _viewConsole.Display($"{AppConsts.Question.ForUpdate.Category}\n{AppConsts.Common.Menu.YesNoSelectable}");
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
            if (choice == "1")
            {
                _viewConsole.Display(AppConsts.Suggestion.Select.CategoryOfGoal);
                categories = _modelConsole.GetCategoriesAsync();
                _viewConsole.OutputCategoryNames(categories);
                return CheckValidation(_viewConsole.GetInput(), categories.Result.Count);
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

        internal async Task CheckAndImplementTheStartMenuItem(string choice)
        {
            if (choice == "1")
                await AddGoalAsync();

            else if (choice == "2")
                ViewGoalList();

            else if (choice == "3")
                await FindGoalAsync();

            else if (choice == "4")
                await UpdateGoalAsync();

            else if (choice == "5")
                await DeleteGoalAsync();

            else if (choice == "6")
                Environment.Exit(0);
        }

        internal async Task StartApplication()
        {
            string choice;
            _viewConsole.Display(AppConsts.Common.Menu.Start + AppConsts.Common.Menu.StartItemSelectable);
            choice = CheckValidation(_viewConsole.GetInput(), AppConsts.Common.NumberOf.StartItems);
            await CheckAndImplementTheStartMenuItem(choice);
        }
    }
}
