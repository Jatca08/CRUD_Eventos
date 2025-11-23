using MauiApp1.Services;
using MauiApp1.Modelos;

namespace MauiApp1.Views
{
    public partial class EventosPage : ContentPage
    {
        private readonly EventoService _servicio;

        public EventosPage()
        {
            InitializeComponent();
            _servicio = new EventoService();
        }

        

        private void OnCrearEventoClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tituloEntry.Text) || string.IsNullOrEmpty(descripcionEditor.Text) || string.IsNullOrEmpty(etiquetasEntry.Text))
            {
                DisplayAlert("Error", "El título es obligatorio.", "OK");
                return;
            }
            var titulo = tituloEntry.Text;
            var descripcion = descripcionEditor.Text;
            var inicio = fechaInicioPicker.Date + horaInicioPicker.Time;
            var fin = fechaFinPicker.Date + horaFinPicker.Time;
            var etiquetas = etiquetasEntry.Text?.Split(',').Select(t => t.Trim()).ToList();

            var nuevoEvento = _servicio.CrearEvento(titulo, descripcion, inicio, fin, etiquetas);
            DisplayAlert("Éxito", $"Evento '{nuevoEvento.Titulo}' creado con ID {nuevoEvento.Id}", "OK");

            // Reiniciar campos
            tituloEntry.Text = string.Empty;
            descripcionEditor.Text = string.Empty;
            fechaInicioPicker.Date = DateTime.Today;
            horaInicioPicker.Time = new TimeSpan(0, 0, 0);
            fechaFinPicker.Date = DateTime.Today;
            horaFinPicker.Time = new TimeSpan(0, 0, 0);
            etiquetasEntry.Text = string.Empty;

        }

        private void OnListarEventosClicked(object sender, EventArgs e)
        {
            var eventos = _servicio.ObtenerEventos();
            var lista = string.Join("\n", eventos.Select(ev => $"{ev.Id}: {ev.Titulo} ({ev.FechaInicio} - {ev.FechaFin})"));
            DisplayAlert("Eventos", lista, "OK");
        }
    }
}