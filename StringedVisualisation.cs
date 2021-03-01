namespace Scale_Trainer
{
    class StringedVisualisation 
    {
        //Stringed instrument;
        public bool[,] ActiveFrets { get; set; } // Координаты ладов, которые в данный момент будут подсвечены
        public Notes.NoteName[,] NameNotes { get; private set; } // Названия нот на грифе	

        public StringedVisualisation(StringedConfig instrument)
        {
            ActiveFrets = new bool[instrument.Strings, instrument.Frets];
            NameNotes = new Notes.NoteName[instrument.Strings, instrument.Frets];
            InitializeNotes(instrument);
        }

        private void InitializeNotes(StringedConfig instrument)
        {
            Notes note = new Notes();
            if()
            //note.SetCurrNote(instrument.Tuning.NameNotes[0]);

            for (int @string = 0; @string < NameNotes.GetLength(0); @string++)
            {
                note.SetCurrNote(instrument.Tuning.NameNotes[@string]);
                Notes.NoteName fretNote = note.GetCurrNote();

                for (int fret = 0; fret < NameNotes.GetLength(1); fret++, fretNote = note.GetNext())
                {
                    NameNotes[@string, fret] = fretNote;
                }
            }
        }
    }
}
