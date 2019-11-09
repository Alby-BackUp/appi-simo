﻿namespace AppiSimo.Client.Pages.Users
{
    using Abstract;
    using Microsoft.AspNetCore.Components;
    using Model.Auth;
    using System;
    using System.Threading.Tasks;

    public class UserDetailComponent : ComponentBase
    {
        [Inject] IGraphQlService<Profile> ProfileService{ get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IViewModelFactory<Profile, ProfileViewModel> ViewModelFactory { get; set; }

        [Parameter] public Guid Key { get; set; }
        
        protected ProfileViewModel ViewModel { get; private set; }
        
        protected override async Task OnParametersSetAsync()
        {
            var profile = Key == default ? new Profile() : await ProfileService.GetOneAsync(Key);
            
            ViewModel = ViewModelFactory.Create(profile);
            StateHasChanged();
        }
        
        protected async Task HandleValidSubmit()
        {
            Console.WriteLine($"Name: {ViewModel.Entity.Name}");
            Console.WriteLine($"Email: {ViewModel.Entity.Email}");
            
            if (ViewModel.IsNew)
                await ProfileService.CreateAsync(ViewModel.Entity);
            else
                await ProfileService.UpdateAsync(ViewModel.Entity);

            NavigationManager.NavigateTo("/users");
        }
    }
}