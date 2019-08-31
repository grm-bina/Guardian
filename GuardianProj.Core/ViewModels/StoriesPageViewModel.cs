using GuardianProj.Core.Interfaces;
using GuardianProj.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuardianProj.Core.ViewModels
{
    public class StoriesPageViewModel : MvxViewModel<String>
    {

        #region fields
        private readonly IHttpService _suplier;
        private SearchResult _searchResult;
        private Dictionary<string, string> _searchParameters;
        private int _currentPage;
        private int _pageSize;
        private List<StoryHeader> _storyHeaders;
        private string _section;
        private readonly IMvxNavigationService _navigationService;

        #endregion

        #region ctor
        public StoriesPageViewModel(IHttpService suplier, IMvxNavigationService navigationService)
        {
            this._navigationService = navigationService;
            _suplier = suplier;
            _currentPage = 1;
            _pageSize = 5;
        }
        #endregion



        #region props
        public List<StoryHeader> StoryHeaders
        {
            get => _storyHeaders;
            private set => SetProperty(ref _storyHeaders, value);
        }

        public int CurrentPage
        {
            get => _currentPage;
            private set => SetProperty(ref _currentPage, value);
        }

        public bool IsPreviousPageAviable
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        public bool IsNextPageAviable
        {
            get
            {
                if (_searchResult == null)
                    return true;
                else
                    return _searchResult.SearchResponse.Pages > CurrentPage;
            }
        }


        #endregion


        #region lifecicle
        public async override Task Initialize()
        {
            SetParameters();
            await GetStories();
        }

        public override void Prepare(string parameter)
        {
            if(!String.IsNullOrEmpty(parameter))
                _section = parameter;

            CloseCommand = new MvxCommand(() => _navigationService.Close(this));

            NextPage = new MvxCommand(async () =>
            {
                if (_searchResult.SearchResponse.Pages > CurrentPage)
                {
                    CurrentPage += 1;
                    await RaisePropertyChanged("IsPreviousPageAviable");
                    await RaisePropertyChanged("IsNextPageAviable");

                    SetParameters();
                    await GetStories();
                }
            });

            PreviousPage = new MvxCommand(async () =>
            {
                if (IsPreviousPageAviable)
                {
                    CurrentPage -= 1;
                    await RaisePropertyChanged("IsPreviousPageAviable");
                    await RaisePropertyChanged("IsNextPageAviable");

                    SetParameters();
                    await GetStories();
                }
            });

            GoToSingleStoryPage = new MvxCommand<string>(async (url) =>
            {
                await _navigationService.Navigate<SingleStoryViewModel, string>(url);
            });


        }

        #endregion

        #region methods
        private void SetParameters()
        {
            _searchParameters = new Dictionary<string, string>();
            _searchParameters.Add(Constants.API_KEY_PARAM, Constants.API_KEY);
            _searchParameters.Add(Constants.PAGE_PARAM, _currentPage.ToString());
            _searchParameters.Add(Constants.PAGE_SIZE_PARAM, _pageSize.ToString());
            _searchParameters.Add(Constants.SHOW_FIELDS_PARAM, "trailText,thumbnail");
            if (!String.IsNullOrEmpty(_section) && _section!= "allNews")
                _searchParameters.Add(Constants.SECTION_PARAM, _section);
        }


        public async Task GetStories()
        {
                var result = await _suplier.GetAsync<SearchResult>(Constants.SEARCH_API_URL, _searchParameters);
                _searchResult = result;
                CleanTrailTextsFromHtmlTags(_searchResult.SearchResponse.StoryHeaders);
                StoryHeaders = _searchResult.SearchResponse.StoryHeaders?.ToList();

        }


        private void CleanTrailTextsFromHtmlTags(StoryHeader[] stories)
        {
            foreach (var item in stories)
            {
                item.StoryHeaderAdditionalFields.CleanTrailTextFromHtmlTags("strong");
                item.StoryHeaderAdditionalFields.CleanTrailTextFromHtmlTags("br");
                item.StoryHeaderAdditionalFields.CleanTrailTextFromHtmlTags("p");
            }
        }


        #endregion

        #region commands
        public IMvxCommand CloseCommand { get; private set; }
        public IMvxCommand PreviousPage { get; private set; }
        public IMvxCommand NextPage { get; private set; }
        public IMvxCommand GoToSingleStoryPage { get; private set; }

        #endregion

    }
}
