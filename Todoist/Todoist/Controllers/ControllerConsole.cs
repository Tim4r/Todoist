using Todoist.Views;
using Todoist.Core.Models;
using Todoist.BL;
using Todoist.Core.Consts;

namespace Todoist.Controllers;

internal class ControllerConsole
{
    private readonly ViewConsole _viewConsole;
    private readonly BusinessLogic _businessLogic;

    internal ControllerConsole(ViewConsole viewConsole, BusinessLogic businessLogic)
    {
        _viewConsole = viewConsole;
        _businessLogic = businessLogic;
    }

    internal async Task CreateGoalAsync()
    {
        string newTitle;
        string newDescription;
        int IdOfSelectedCategory;
        string selectedStatus;
        
        newTitle = CreateTitleOfGoal();
        newDescription = CreateDescriptionOfGoal();
        IdOfSelectedCategory = SelectCategory((await _businessLogic.GetCategoriesAsync()).ToList());
        selectedStatus = SelectStatus(_businessLogic.GetStatuses());
        
        await _businessLogic.CreateGoalAsync(newTitle, newDescription, selectedStatus, IdOfSelectedCategory);
        _viewConsole.Display(AppConsts.Common.TaskAdded);
    }

    internal async Task ViewGoals()
    {
        IEnumerable<Category> categories = await _businessLogic.GetCategoriesAsync();
        IEnumerable<Goal> goals = await _businessLogic.GetGoalsAsync();
        _viewConsole.OutputCategories(categories.ToList(), goals.ToList());
    }
    
    internal async Task FindGoalAsync()
    {
        IEnumerable<Goal> goals = await _businessLogic.GetGoalsAsync();
        List<Goal> results;
        string searchWord;

        _viewConsole.Display(AppConsts.Suggestion.Enter.WordForSearch);
        do
        {
            searchWord = _viewConsole.GetInput();
            if (!(searchWord.Length <= AppConsts.Common.NumberOf.MaximumCharactersForDescription)) _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
        }
        while (!(searchWord.Length <= AppConsts.Common.NumberOf.MaximumCharactersForDescription));

        results = _businessLogic.SearchByTitleDescription(goals.ToList(), searchWord).ToList();
        if (results.Count == 0)
            _viewConsole.Display(AppConsts.Suggestion.Enter.NotFound);
        else
            _viewConsole.OutputGoals(results);
    }

    internal async Task UpdateGoalAsync()
    {
        List<Goal> goals = (await _businessLogic.GetGoalsAsync()).ToList();
        string choice;

        _viewConsole.Display(AppConsts.Suggestion.Select.Goal + "\n");
        _viewConsole.OutputGoals(goals);
        choice = CheckValidate(_viewConsole.GetInput(), goals.Count);
        var goalForUpdate = _businessLogic.SearchGoal(goals, Convert.ToInt32(choice));
        _viewConsole.Display(goalForUpdate);

        string titleOfGoal = GetNewTitleOfGoal();
        string descriptionOfGoal = GetNewDescriptionOfGoal();
        string categoryOfGoal = await GetNewIDCategoryOfGoal();
        string statusOfGoal = GetNewStatusOfGoal();
        
        await _businessLogic.UpdateGoalAsync(goalForUpdate, titleOfGoal, descriptionOfGoal, categoryOfGoal, statusOfGoal);
        _viewConsole.Display(AppConsts.Common.TaskChanged);
    }

    internal async Task DeleteGoalAsync()
    {
        List<Goal> Goals = (await _businessLogic.GetGoalsAsync()).ToList();
        string choice;

        _viewConsole.Display(AppConsts.Suggestion.Select.Goal);
        _viewConsole.OutputGoals(Goals);

        choice = CheckValidate(_viewConsole.GetInput(), Goals.Count);

        var searchedElementGoal = _businessLogic.SearchGoal(Goals, Convert.ToInt32(choice));
        _viewConsole.Display(searchedElementGoal);

        _viewConsole.Display(AppConsts.Question.ForDelete.Confirmation + AppConsts.Common.Menu.YesNoSelectable);
        choice = CheckValidate(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);

        if (choice == "1")
        {
            await _businessLogic.DeleteGoalAsync(searchedElementGoal);
            _viewConsole.Display(AppConsts.Common.TaskDelete);
        }
    }


    internal string CreateTitleOfGoal()
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

    internal string CreateDescriptionOfGoal()
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

    internal int SelectCategory(List<Category> categories)
    {
        string selectedCategory;
        _viewConsole.Display(AppConsts.Suggestion.Select.CategoryOfGoal);
        _viewConsole.OutputCategoryNames(categories);
        selectedCategory = CheckValidate(_viewConsole.GetInput(), categories.Count);
        return categories[Convert.ToInt32(selectedCategory) - 1].Id;
    }

    internal string SelectStatus(string[] statuses)
    {
        string selectedStatus;
        _viewConsole.Display(AppConsts.Suggestion.Select.StatusOfGoal);
        _viewConsole.OutputOfAvaliableStatuses(statuses);
        selectedStatus = CheckValidate(_viewConsole.GetInput(), statuses.Length);
        return _businessLogic.GetStatus(selectedStatus);
    }


    internal string GetNewTitleOfGoal()
    {
        string choice;
        string newTitle;

        _viewConsole.Display($"{AppConsts.Question.ForUpdate.Title}\n{AppConsts.Common.Menu.YesNoSelectable}");
        choice = CheckValidate(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
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
        choice = CheckValidate(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
        if (choice == "1")
        {
            _viewConsole.Display(AppConsts.Suggestion.Enter.NewDescription);
            newDescription = _viewConsole.GetInput();
            return newDescription;
        }
        else
            return _viewConsole.GetEmpty();
    }

    internal async Task<string> GetNewIDCategoryOfGoal()
    {
        List<Category> categories;
        string choice;
        _viewConsole.Display($"{AppConsts.Question.ForUpdate.Category}\n{AppConsts.Common.Menu.YesNoSelectable}");
        choice = CheckValidate(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
        if (choice == "1")
        {
            _viewConsole.Display(AppConsts.Suggestion.Select.CategoryOfGoal);
            categories = (await _businessLogic.GetCategoriesAsync()).ToList();
            _viewConsole.OutputCategoryNames(categories);
            return CheckValidate(_viewConsole.GetInput(), categories.Count);
        }
        else
            return _viewConsole.GetEmpty();
    }

    internal string GetNewStatusOfGoal()
    {
        string[] statuses;
        string choice;

        _viewConsole.Display($"{AppConsts.Question.ForUpdate.Status}\n{AppConsts.Common.Menu.YesNoSelectable}");
        choice = CheckValidate(_viewConsole.GetInput(), AppConsts.Common.NumberOf.YesOrNoItems);
        if (choice == "1")
        {
            _viewConsole.Display(AppConsts.Suggestion.Select.StatusOfGoal);
            statuses = _businessLogic.GetStatuses();
            _viewConsole.OutputOfAvaliableStatuses(statuses);
            choice = CheckValidate(_viewConsole.GetInput(), statuses.Length);
            return _businessLogic.GetStatus(choice);
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

    internal string CheckValidate(string choice, int numberOfElementsMenu)
    {
        while (!Validation(choice, numberOfElementsMenu))
        {
            _viewConsole.Display(AppConsts.Suggestion.Enter.ValidValue);
            choice = _viewConsole.GetInput();
        }
        return choice;
    }

    internal async Task DisplayStartMenu(string choice)
    {
        if (choice == "1")
            await CreateGoalAsync();

        else if (choice == "2")
            await ViewGoals();

        else if (choice == "3")
            await FindGoalAsync();

        else if (choice == "4")
            await UpdateGoalAsync();

        else if (choice == "5")
            await DeleteGoalAsync();

        else if (choice == "6")
            Environment.Exit(0);
    }
}
