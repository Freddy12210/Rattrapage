namespace Rattrapage
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MainPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;   
        }

        private async void OnValidateClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            var year = YearEntry.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(year))
            {
                await DisplayAlert("Erreur", "Veuillez entrer à la fois le prénom et l'année", "OK");
                return;
            }

            try
            {
                string apiUrl = $"https://data.nantesmetropole.fr/api/explore/v2.1/catalog/datasets/244400404_prenoms-enfants-nes-nantes/records?select=count(*)&where=enfant_prenom%3D%27{name}%27%20and%20annee%20%3D{year}&group_by=commune_nom&limit=20";

                string data = await _apiService.GetDataAsync(apiUrl);
                DataLabel.Text = data;
                SemanticScreenReader.Announce(DataLabel.Text); // Annonce pour les lecteurs d'écran
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Une erreur s'est produite : {ex.Message}", "OK");
            }
        }
    }

}
