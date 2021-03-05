using System;

namespace Scale_Trainer
{
    internal sealed class StringedVisualisation
    {
        public bool[,] ActiveFrets { get; set; } // Координаты ладов, которые в данный момент будут подсвечены
        public Note[,] Notes { get; private set; } // Ноты на грифе	с учётом строя
        private readonly StringedConfig instrument;
        private readonly StringedVisualizationConfig config;
        private readonly Scale scale;
        private int curString = 0;
        private int curFret = 0;
        public bool EndOfExercise = false;

        public StringedVisualisation(StringedConfig instrument, StringedVisualizationConfig config, Scale scale)
        {
            Validate.IsTrue(instrument.Strings >= config.FirstString, "Настройка первой струны больше количества струн инструмента.");
            this.instrument = instrument;
            this.config = config;
            this.scale = scale;
            ActiveFrets = new bool[instrument.Strings, instrument.Frets];
            Notes = new Note[instrument.Strings, instrument.Frets];
            InitializeNotes();
            SetFirstCoord();
        }

        private void InitializeNotes()
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

        private void SetFirstCoord()
        {
            for (int fret = 0; fret < instrument.Frets; fret++)
            {
                if (Notes[config.FirstString - 1, fret].CurNote == scale.Key)
                {
                    curString = config.FirstString - 1;
                    curFret = fret;
                    ActiveFrets[curString, curFret] = true;
                    return;
                }
            }
            throw new Exception("Нота не найдена.");
        }

        public void SetScaleSector()
        {
            int startPosFret = curFret;
            int interval = scale.NextInterval();

            for (int i = 0; i < config.NotesOnNeck; i++, interval = scale.NextInterval())
            {
                if (curFret - startPosFret >= config.MaxInterval)
                {
                    int neededFret = FindNeededFretOnNextString(interval, startPosFret);
                    JumpToNextString(neededFret);
                    startPosFret = neededFret;
                }
                else
                {
                    JumpToNextFret(interval);
                }
            }
        }

        private int FindNeededFretOnNextString(int interval, int startPosFret, bool reverse = false)
        {
            Note neededNote = new Note(Notes[curString, curFret]);
            neededNote.OffsetNote(interval);
            int tempString;

            if (!reverse)
            {
                tempString = curString - 1;
            }
            else
            {
                tempString = curString + 1;
            }

            for (int fret = startPosFret - 1; fret < startPosFret + config.MaxInterval; fret++)
            {
                if (Notes[tempString, fret] == neededNote)
                {
                    return fret;
                }
            }
            throw new Exception("В заданном диапазоне нота не найдена. Измените настройки диапазона.");
        }

        private void JumpToNextFret(int interval, bool reverse = false)
        {
            if (!CheckEdgeOfNeck())
            {
                if (!reverse)
                {
                    curFret += interval;
                }
                else
                {
                    curFret -= interval;
                }
                ActiveFrets[curString, curFret] = true;
            }
            else
            {
                EndOfExercise = true;
            }
        }

        private void JumpToNextString(int fret, bool reverse = false)
        {
            curFret = fret;
            if (!reverse)
            {
                curString--;
            }
            else
            {
                curString++;
            }
            ActiveFrets[curString, fret] = true;
        }

        private bool CheckEdgeOfNeck()
        {
            return curFret > instrument.Frets || curFret < 0;
        }

    }
}
