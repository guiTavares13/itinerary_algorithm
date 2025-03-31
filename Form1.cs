using System.Formats.Asn1;
using System.Globalization;
using System;
using poc_recommended_trip.Models;
using CsvHelper.Configuration;
using CsvHelper;
using poc_recommended_trip.Dao;
using poc_recommended_trip.Controllers;
using System.Threading.Tasks;
using poc_recommended_trip.MachineLearning;
using System.Diagnostics.Metrics;

namespace poc_recommended_trip
{
    public partial class Form1 : Form
    {
        private DestinationsDao destinationsDao = new DestinationsDao();
        private PlacesInfoDao placesInfoDao = new PlacesInfoDao();

        public Form1()
        {
            InitializeComponent();
            Database.Initialize();

            if (!HasData())
            {
                LoadCsvData();
            }
        }

        private bool HasData()
        {
            var destinations = destinationsDao.ListAll();
            return destinations.Count > 0;
        }

        public void LoadCsvData()
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(exeDirectory, "destinations.csv");

            if (!File.Exists(path))
            {
                MessageBox.Show("O arquivo 'destinations.csv' não foi encontrado na pasta do executável.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var records = csv.GetRecords<DestinationModel>();

            foreach (var record in records)
            {
                destinationsDao.Insert(record);
            }

            MessageBox.Show("Dados do CSV carregados no banco de dados SQLite.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstDestinos.Items.Clear();

            var destinations = destinationsDao.ListAll();

            if (destinations.Count == 0)
            {
                lstDestinos.Items.Add("Nenhum destino encontrado.");
                return;
            }

            foreach (var dest in destinations)
            {
                lstDestinos.Items.Add($"Destino: {dest.Destination}, Região: {dest.Region} País: {dest.Country}, " +
                    $"Turistas por ano: {dest.ApproximateAnnualTourists}, Moeda: {dest.Currency}, Religião: {dest.MajorityReligion}" +
                    $"Comida: {dest.FamousFoods}, Lingua: {dest.Language}, Melhor Epoca Visita: {dest.BestTimeToVisit}");
            }
        }

        //private async void btnGetInfo_Click_1(object sender, EventArgs e)
        //{
        //    string destinationName = txtDestination.Text.Trim();

        //    if (string.IsNullOrEmpty(destinationName))
        //    {
        //        MessageBox.Show("❌ Digite um nome de destino!");
        //        return;
        //    }

        //    PlacesInfoController controller = new PlacesInfoController();
        //    PlacesInfoModel placeInfo = await controller.GetOrFetchPlaceInfo(destinationName);

        //    if (placeInfo != null)
        //    {
        //        MessageBox.Show($"📍 {placeInfo.Name} - {placeInfo.DisplayName} ({placeInfo.Latitude}, {placeInfo.Longitude})");
        //        lstPlacesInfo.Items.Add($"📍 {placeInfo.Name} - {placeInfo.DisplayName} ({placeInfo.Latitude}, {placeInfo.Longitude})");
        //    }
        //}

        private void btnKmeans_Click(object sender, EventArgs e)
        {
            string destinationName = txtDestination.Text.Trim();
            int days;

            lbRoteiro.Items.Clear();

            if (!int.TryParse(txtDays.Text.Trim(), out days) || days <= 0)
            {
                MessageBox.Show("Por favor, insira um número válido de dias.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var kMeans = new KMeansClustering();
            var itineraries = kMeans.ClusterDestinations(destinationName, days);

            if (itineraries.Count == 0)
            {
                MessageBox.Show("Nenhum destino encontrado para o país especificado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Adiciona o título do roteiro
            lbRoteiro.Items.Add("📍 Roteiro de viagem:");

            // Adiciona cada dia e seus destinos como itens separados
            for (int i = 0; i < itineraries.Count; i++)
            {
                lbRoteiro.Items.Add($"Dia {i + 1}:");
                foreach (var destination in itineraries[i])
                {
                    lbRoteiro.Items.Add($"  - {destination.Destination} ({destination.Region})");
                }
                lbRoteiro.Items.Add(""); // Linha em branco para separar os dias
            }

            // Opcional: Exibir uma mensagem de sucesso
            MessageBox.Show("Roteiro gerado com sucesso!", "Roteiro Gerado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
