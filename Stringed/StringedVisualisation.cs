using System;

namespace Scale_Trainer
{
    internal sealed class StringedVisualisation
    {
        public bool[,] ActiveFrets { get; set; } // Координаты ладов, которые в данный момент будут подсвечены
        public Note[,] Notes { get; private set; } // Ноты на грифе	с учётом строя
        private readonly StringedConfig instrument;
        private readonly StringedVisualizationConfig config;       

        public StringedVisualisation(StringedConfig instrument, StringedVisualizationConfig config)
        {
            Validate.IsTrue(instrument.Strings >= config.FirstString, "Настройка первой струны больше количества струн инструмента.");
            this.instrument = instrument;
            this.config = config;
            ActiveFrets = new bool[instrument.Strings, instrument.Frets];
            Notes = new Note[instrument.Strings, instrument.Frets];
            InitializeNotes(instrument);
        }

        private void InitializeNotes(StringedConfig instrument)
        {
            for (int @string = 0; @string < instrument.Strings; @string++)
            {
                Note note = new Note(instrument.Tuning.Notes[@string].CurNote, instrument.Tuning.Notes[@string].Octave);

                for (int fret = 0; fret < instrument.Frets; fret++, note.NextNote())
                {
                    Notes[@string, fret] = new Note(note);
                }
            }
        }

        public void SetFirstCoord(Note.NoteName key)
        {
            for (int fret = 0; fret < instrument.Frets; fret++)
            {
                if (Notes[config.FirstString - 1, fret].CurNote == key)
                {
                    ActiveFrets[config.FirstString - 1, fret] = true;
                    return;
                }
            }
            throw new Exception("Нота не найдена.");
        }
    }
}
