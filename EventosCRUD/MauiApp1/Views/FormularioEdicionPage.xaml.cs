using MauiApp1.Modelos;
using MauiApp1.Services;
using System;
using System.Linq;

namespace MauiApp1.Views
{
    public partial class FormularioEdicionPage : ContentPage
    {
        // Campos locales con nombres distintos a los definidos automáticamente por XAML
        private Entry _tituloEntry;
        private Editor _descripcionEditor;
        private DatePicker _fechaInicioPicker;
        private TimePicker _horaInicioPicker;
        private DatePicker _fechaFinPicker;
        private TimePicker _horaFinPicker;
        private Entry _etiquetasEntry;

        private readonly EventoService _servicio;
        private readonly Evento _evento;

        public FormularioEdicionPage(Evento evento)
        {
            InitializeComponent();
            _servicio = EventoService.Instancia;

            _evento = evento;

            // Obtener controles por nombre para evitar ambigüedad con miembros generados
            _tituloEntry = this.FindByName<Entry>("tituloEntry");
            _descripcionEditor = this.FindByName<Editor>("descripcionEditor");
            _fechaInicioPicker = this.FindByName<DatePicker>("fechaInicioPicker");
            _horaInicioPicker = this.FindByName<TimePicker>("horaInicioPicker");
            _fechaFinPicker = this.FindByName<DatePicker>("fechaFinPicker");
            _horaFinPicker = this.FindByName<TimePicker>("horaFinPicker");
            _etiquetasEntry = this.FindByName<Entry>("etiquetasEntry");

            // Cargar datos en los controles
            if (_tituloEntry != null) _tituloEntry.Text = evento.Titulo;
            if (_descripcionEditor != null) _descripcionEditor.Text = evento.Descripcion;
            if (_fechaInicioPicker != null) _fechaInicioPicker.Date = evento.FechaInicio.Date;
            if (_horaInicioPicker != null) _horaInicioPicker.Time = evento.FechaInicio.TimeOfDay;
            if (_fechaFinPicker != null) _fechaFinPicker.Date = evento.FechaFin.Date;
            if (_horaFinPicker != null) _horaFinPicker.Time = evento.FechaFin.TimeOfDay;
            if (_etiquetasEntry != null) _etiquetasEntry.Text = string.Join(",", evento.Etiquetas);
        }

        private async void OnGuardarCambiosClicked(object sender, EventArgs e)
        {
            var titulo = _tituloEntry?.Text;
            var descripcion = _descripcionEditor?.Text;
            var etiquetas = _etiquetasEntry?.Text;

            if (string.IsNullOrWhiteSpace(titulo) ||
                string.IsNullOrWhiteSpace(descripcion) ||
                string.IsNullOrWhiteSpace(etiquetas))
            {
                await DisplayAlert("Error", "Todos los campos deben estar completos.", "OK");
                return;
            }

            _evento.Titulo = titulo;
            _evento.Descripcion = descripcion;

            if (_fechaInicioPicker != null && _horaInicioPicker != null)
                _evento.FechaInicio = _fechaInicioPicker.Date + _horaInicioPicker.Time;
            if (_fechaFinPicker != null && _horaFinPicker != null)
                _evento.FechaFin = _fechaFinPicker.Date + _horaFinPicker.Time;

            _evento.Etiquetas = etiquetas.Split(',').Select(t => t.Trim()).ToList();

            // Usar el método EditarEvento en el servicio
            _servicio.EditarEvento(_evento.Id, _evento.Titulo, _evento.Descripcion, _evento.FechaInicio, _evento.FechaFin, _evento.Etiquetas);

            await DisplayAlert("Éxito", "Evento actualizado correctamente.", "OK");
            await Navigation.PopAsync();
        }
    }
}