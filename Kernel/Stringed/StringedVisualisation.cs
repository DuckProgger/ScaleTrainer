using System;

namespace Scale_Trainer
{
    internal sealed class StringedVisualisation
    {
        public bool[,] AvailableFrets { get; private set; } // Доступные лады в соответствии с гаммой и строем гитары
        private Note[,] notes; // Ноты на грифе	с учётом строя
        private readonly StringedInstrument instrument;
        private readonly Scale scale;

        public StringedVisualisation(StringedInstrument instrument, Scale scale)
        {
            this.instrument = instrument;
            this.scale = scale;
            notes = new Note[instrument.Strings, instrument.Frets + 1];
            AvailableFrets = new bool[instrument.Strings, instrument.Frets + 1];
            InitializeNotes();
            InitializeAvailableFrets();
        }

        private void InitializeNotes()
        {
            for (int @string = 0; @string < instrument.Strings; @string++)
            {
                Note note = new Note(instrument.Tuning.Notes[@string].CurNote, instrument.Tuning.Notes[@string].Octave);

                for (int fret = 0; fret <= instrument.Frets; fret++, note.NextNote())
                {
                    notes[@string, fret] = new Note(note);
                }
            }
        }

        private void InitializeAvailableFrets()
        {
            // Найти первые номера ладов всех струн, на которых совпадают ноты этих ладов и какой-либо ноты из гаммы
            for (int @string = 0; @string < instrument.Strings; @string++)
            {
                FindFirstCoincidentFretOnString(@string);
            }

            void FindFirstCoincidentFretOnString(int @string)
            {
                Note fretNote = instrument.Tuning.Notes[@string];
                for (int fret = 0; fret < 12; fret++, fretNote.NextNote()) // на октаве по-любому найдётся совпадение с нотой гаммы
                {
                    Scale tempScale = new Scale(scale); // скопировать гамму, чтобы не измененять основную в цикле

                    foreach (Note scaleNote in tempScale)
                    {
                        if (fretNote.CurNote == scaleNote.CurNote)
                        {
                            SetAvailableFrets(@string, fret, tempScale);
                            break;
                        }
                    }
                    if (AvailableFrets[@string, fret])
                    {
                        return;
                    }
                }
            }

            // Найти все лады, соответствующие гамме, начиная с первого совпадающего лада
            void SetAvailableFrets(int @string, int startFret, Scale scale) // в гамме, переданной в качестве аргумента, текущая нота соответствует ноте текущего лада
            {
                Note seachedNote = scale.GetCurrentNote();

                for (int fret = startFret; fret <= instrument.Frets; fret++)
                {
                    if (notes[@string, fret].CurNote == seachedNote.CurNote)
                    {
                        AvailableFrets[@string, fret] = true;
                        scale.MoveNext();
                        seachedNote = scale.GetCurrentNote();
                    }
                }

            }
        }
    }
}

