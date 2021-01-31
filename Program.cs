using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Carlton.TestBed.Utils;
using Carlton.Base.Client.Components;
using Carlton.Base.Client.Components.TestData;
using Carlton.Base.Client.Components.Test;
using Carlton.Dashboard.Models.TestModels;
using Carlton.Dashboard.Components;

namespace Carlton.Dashboard.TestBed
{
    public static class Program
    {
        private const string BASE_COMPONENTS = "Base Components";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var sourceBasePath = builder.Configuration.GetSection("sourceSamplesSettings").GetValue<string>("sourceBasePath");

            builder.AddCarltonTestBed(builder =>
            {
                //Base Components
                builder.AddComponent<Card>($"{BASE_COMPONENTS}/Cards/Card", CardTestStates.DefaultState());
                builder.AddComponent<ListCard<string>>($"{BASE_COMPONENTS}/Cards/ListCard", CardTestStates.DefaultListState());
                builder.AddComponent<Header>($"{BASE_COMPONENTS}/Header");
                builder.AddComponent<Logo>($"{BASE_COMPONENTS}/Logo");
                builder.AddComponent<HamburgerCollapser>($"{BASE_COMPONENTS}/HamburgerCollapser");
                builder.AddComponent<UserProfile>($"{BASE_COMPONENTS}/UserProfile");
                builder.AddComponent<Checkbox>($"{BASE_COMPONENTS}/checkbox/Checked", CheckboxTestStates.CheckedState());
                builder.AddComponent<Checkbox>($"{BASE_COMPONENTS}/checkbox/Unchecked", CheckboxTestStates.UncheckedState());
                builder.AddComponent<Select>($"{BASE_COMPONENTS}/Select/Default", SelectTestStates.Default());
                builder.AddComponent<ResizablePanel>($"{BASE_COMPONENTS}/ResizablePanel/Default");
                builder.AddComponent<Select>($"{BASE_COMPONENTS}/Select/Default", SelectTestStates.Default());
                //ToDos
                builder.AddCarltonComponent<ToDoListItem>("ToDos/ListItem/Checked", ToDoListTestModels.ToDoListItemCheckedModel());
                builder.AddCarltonComponent<ToDoListItem>("ToDos/ListItem/Unchecked", ToDoListTestModels.ToDoListItemUncheckedModel());
                builder.AddCarltonComponent<ToDoListCard>("ToDos/ListCard/Default", ToDoListTestModels.DefaultToDoListModel());
                //ApartmentStatus
                builder.AddCarltonComponent<ApartmentStatusListItem>("ApartmentStatus/ListItem/Completed", ApartmentStatusTestModels.CompletedStatusModel());
                builder.AddCarltonComponent<ApartmentStatusListItem>("ApartmentStatus/ListItem/Incomplete", ApartmentStatusTestModels.InCompleteStatusModel());
                builder.AddCarltonComponent<ApartmentStatusListCard>("ApartmentStatus/ListCard/Default", ApartmentStatusTestModels.DefaultApartmentStatusModel());
                //DinnerGuests
                builder.AddCarltonComponent<DinnerGuestsSelfStatus>("DinnerGuests/SelfStatus/HomeForDinner", DinnerGuestsTestModels.DinnerGuestsSelfHomeModel());
                builder.AddCarltonComponent<DinnerGuestsSelfStatus>("DinnerGuests/SelfStatus/NotHomeForDinner", DinnerGuestsTestModels.DinnerGuestsSelfNotHomeModel());
                builder.AddCarltonComponent<DinnerGuestsListItem>("DinnerGuests/ListItem/HomeForDinner", DinnerGuestsTestModels.DinnerGuestHomeModel());
                builder.AddCarltonComponent<DinnerGuestsListItem>("DinnerGuests/ListItem/NotHomeForDinner", DinnerGuestsTestModels.DinnerGuestNotHomeModel());
                builder.AddCarltonComponent<DinnerGuestsListCard>("DinnerGuests/ListCard", DinnerGuestsTestModels.DefaultHomeForDinnerModel());
                //Groceries
                builder.AddCarltonComponent<GroceriesListItem>("Groceries/ListItems/Low", GroceriesTestModels.GroceriesLowListItemModel());
                builder.AddCarltonComponent<GroceriesListItem>("Groceries/ListItems/Medium", GroceriesTestModels.GroceriesMediumListItemModel());
                builder.AddCarltonComponent<GroceriesListItem>("Groceries/ListItems/High", GroceriesTestModels.GroceriesHighListItemModel());
                builder.AddCarltonComponent<GroceriesListCard>("Groceries/ListCard/Default", GroceriesTestModels.DefaultGroceriesListModel());
                //Feed
                builder.AddCarltonComponent<FeedListItem>("Feed/ListItem", FeedListTestModels.DefaultFeedListItemModel());
                builder.AddCarltonComponent<FeedListCard>("Feed/ListCard", FeedListTestModels.DefaultFeedItemListModel());
                //CountCards
                builder.AddCarltonComponent<ToDosCountCard>("CountCards/ToDos/Default", DashboardAggregationsTestModels.DefaultDashboardAggregationModel());
                builder.AddCarltonComponent<ApartmentStatusCountCard>("CountCards/ApartmentStatus/Default", DashboardAggregationsTestModels.DefaultDashboardAggregationModel());
                builder.AddCarltonComponent<DinnerGuestsCountCard>("CountCards/DinnerGuesets/Default", DashboardAggregationsTestModels.DefaultDashboardAggregationModel());
                builder.AddCarltonComponent<GroceriesCountCard>("CountCards/Groceries/Default", DashboardAggregationsTestModels.DefaultDashboardAggregationModel());
            },
            sourceBasePath,
            typeof(Carlton.TestBed.Pages.TestBed).Assembly);


            builder.Services.AddScoped(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                });


            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync().ConfigureAwait(true);
        }
    }
}
