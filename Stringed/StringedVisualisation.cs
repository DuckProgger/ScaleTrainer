namespace Scale_Trainer
{
    internal sealed class StringedVisualisation
    {
        public bool[,] ActiveFrets { get; set; } // Координаты ладов, которые в данный момент будут подсвечены
        public Note[,] Notes { get; private set; } // Ноты на грифе	с учётом строя

        public StringedVisualisation(StringedConfig instrument)
        {
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
    }
}
