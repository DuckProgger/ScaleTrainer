using System;

namespace Scale_Trainer
{
    internal sealed class StringedVisualisation
    {
        public bool[,] ActiveFrets { get; set; } // Координаты ладов, которые в данный момент будут подсвечены
        public Note[,] Notes { get; private set; } // Ноты на грифе	с учётом строя
        public bool EndOfExercise { get; private set; } = false;

        private readonly StringedConfig instrument;
        private readonly StringedVisualizationConfig config;
        private readonly Scale scale;
        private int curString = 0;
        private int curFret = 0;
        private bool reverse = false;

        public StringedVisualisation(StringedConfig instrument, StringedVisualizationConfig config, Scale scale)
        {
            Validate.IsTrue(instrument.Strings >= config.FirstString, "Настройка первой струны больше количества струн инструмента.");
            this.instrument = instrument;
            this.config = config;
            this.scale = scale;
            ActiveFrets = new bool[instrument.Strings, instrument.Frets];
            Notes = new Note[instrument.Strings, instrument.Frets];
            InitializeNotes();
            FindAndSetFirstFret();
            SetFirstFrets();
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

        private void FindAndSetFirstFret()
        {
            for (int fret = 0; fret < instrument.Frets; fret++)
            {
                if (Notes[config.FirstString - 1, fret].CurNote == scale.Key)
                {
                    curString = config.FirstString - 1;
                    curFret = fret;
                    SetFret(curString, curFret);
                    return;
                }
            }
            throw new Exception("Нота не найдена.");
        }

        private void SetFirstFrets()
        {

            int startPosFret = curFret;

            for (int i = 0; i < config.NotesOnNeck - 1; i++)
            {
                int interval = scale.MoveNext();

                if (NextStringCondition(startPosFret))
                {
                    SetNextFretOnNextString(interval);
                    startPosFret = curFret;
                }
                else
                {
                    SetNextFret(interval);
                }
            }
        }

        public void SetFretsNextString()
        {
            int @string = reverse ? curString - 1 : curString + 1;
            RemoveFretsOnString(@string);

            if (CheckEndString())
            {
                reverse = !reverse;
                @string = reverse ? curString + 1 : curString - 1;
                RemoveFretsOnString(@string);
                RemoveFretsOnString(curString);
                SetFirstFrets();
            }
            else
            {                
                int interval = scale.MoveNext();
                SetNextFretOnNextString(interval);

                int startPosFret = curFret;
                while (!NextStringCondition(startPosFret))
                {
                    interval = scale.MoveNext();
                    SetNextFret(interval);
                }
            }

            //int interval = scale.MoveNext();
            //if (JumpToNextString(interval))
            //{
            //    // Достигли крайней струны
            //    @string = reverse ? curString + 1 : curString - 1;
            //    RemoveFretsOnString(@string);
            //    RemoveFretsOnString(curString);
            //    JumpToNextFret(interval);
            //    SetFirstFrets();
            //}
            //else
            //{
            //    // Переход на следущую струну
            //    int startPosFret = curFret;
            //    while (true)
            //    {
            //        JumpToNextFret(interval);
            //        if (NextStringCondition(startPosFret))
            //        {
            //            break;
            //        }
            //        interval = scale.MoveNext();
            //    }
            //}
        }

        private void RemoveFretsOnString(int @string)
        {
            for (int i = 0; i < instrument.Frets; i++)
            {
                ResetFret(@string, i);
            }
        }

        private int FindFretByNote(Note note, int @string)
        {           
            if (!reverse)
            {
                for (int fret = curFret - config.MaxInterval - 1; fret <= curFret; fret++)
                {
                    if (Notes[@string, fret] == note)
                    {
                        return fret;
                    }
                }
            }
            else
            {
                for (int fret = curFret + config.MaxInterval + 1; fret >= curFret; fret--)
                {
                    if (Notes[@string, fret] == note)
                    {
                        return fret;
                    }
                }
            }

            throw new Exception("В заданном диапазоне нота не найдена. Измените настройки диапазона.");
        }

        //private int FindNeededFretOnNextString(int interval)
        //{
        //    Note neededNote = new Note(Notes[curString, curFret]);
        //    neededNote.OffsetNote(interval, reverse);
        //    int tempString;

        //    tempString = reverse ? curString + 1 : curString - 1;
        //    if (!reverse)
        //    {
        //        for (int fret = curFret - config.MaxInterval - 1; fret <= curFret; fret++)
        //        {
        //            if (Notes[tempString, fret] == neededNote)
        //            {
        //                return fret;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int fret = curFret + config.MaxInterval + 1; fret >= curFret; fret--)
        //        {
        //            if (Notes[tempString, fret] == neededNote)
        //            {
        //                return fret;
        //            }
        //        }
        //    }

        //    throw new Exception("В заданном диапазоне нота не найдена. Измените настройки диапазона.");
        //}

        private void SetNextFretOnNextString(int interval)
        {
            Note neededNote = new Note(Notes[curString, curFret]);
            neededNote.OffsetNote(interval, reverse);
            JumpToNextString();
            curFret = FindFretByNote(neededNote, curString);
            SetFret(curString, curFret);
        }

        private void SetNextFret(int interval)
        {
            JumpToNextFret(interval);
            SetFret(curString, curFret);
        }

        private void JumpToNextFret(int interval)
        {
            curFret = reverse ? curFret - interval : curFret + interval;
        }

        private void JumpToNextString()
        {
            curString = reverse ? ++curString : --curString;
        }

        //private bool JumpToNextString(int interval)
        //{
        //    curString = reverse ? ++curString : --curString;
        //    if (CheckEndString())
        //    {
        //        reverse = !reverse;
        //        return true;
        //    }
        //    curFret = FindNeededFretOnNextString(interval);
        //    SetActiveFret(curString, curFret);
        //    return false;
        //}

        private bool CheckEdgeOfNeck() => curFret > instrument.Frets || curFret < 0;

        private bool CheckEndString() => curString >= instrument.Strings - 1 || curString <= 0;


        //private bool CheckEndString()
        //{
        //    int nextString = reverse ? ++curString : --curString;
        //    return nextString >= instrument.Strings - 1 || nextString <= 0;
        //}

        private bool NextStringCondition(int startPosFret) => CheckMaxInterval(startPosFret) || CheckNotesOnString();

        private bool CheckMaxInterval(int startPosFret) => reverse ? startPosFret - curFret >= config.MaxInterval : curFret - startPosFret >= config.MaxInterval;

        private bool CheckNotesOnString()
        {
            int activeFretsCount = 0;

            if (!reverse)
            {
                for (int fret = curFret - config.MaxInterval - 1; fret <= curFret; fret++)
                {
                    if (ActiveFrets[curString, fret])
                    {
                        activeFretsCount++;
                    }
                }
            }
            else
            {
                for (int fret = curFret + config.MaxInterval + 1; fret >= curFret; fret--)
                {
                    if (ActiveFrets[curString, fret])
                    {
                        activeFretsCount++;
                    }
                }
            }

            if (activeFretsCount >= config.NotesOnString)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetFret(int @string, int fret)
        {
            ActiveFrets[@string, fret] = true;
        }

        private void ResetFret(int @string, int fret)
        {
            ActiveFrets[@string, fret] = false;
        }

    }
}
