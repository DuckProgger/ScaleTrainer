namespace Scale_Trainer
{
    class StringedVisualisation 
    {
        //Stringed instrument;
        public bool[,] ActiveFrets { get; set; } // Координаты ладов, которые в данный момент будут подсвечены
        public int[,] Notes { get; private set; } // Названия нот на грифе	

        public StringedVisualisation(Stringed instrument)
        {
            ActiveFrets = new bool[instrument.Strings, instrument.Frets];
            Notes = new int[instrument.Strings, instrument.Frets];
            InitializeNotes(instrument);
        }

        private void InitializeNotes(Stringed instrument)
        {
            Notes note = new Notes();
            //note.SetCurrNote(instrument.Tuning.Notes[0]);
            //int fretNote = note.GetCurrNote();

            for (int @string = 0; @string < Notes.GetLength(0); @string++)
            {
                note.SetCurrNote(instrument.Tuning.Notes[@string]);
                int fretNote = note.GetCurrNote();

                for (int fret = 0; fret < Notes.GetLength(1); fret++, fretNote = note.GetNext())
                {
                    Notes[@string, fret] = fretNote;
                }
            }
        }
    }
}
