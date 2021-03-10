using System;

namespace Scale_Trainer
{
    internal sealed class StringedVisualisation
    {
        //public bool[,] ActiveFrets { get; set; } // Координаты ладов, которые в данный момент будут подсвечены
        public Note[,] Notes { get; private set; } // Ноты на грифе	с учётом строя
        //public bool EndOfExercise { get; private set; } = false;

        private readonly StringedConfig instrument;
        //private readonly StringedVisualizationConfig config;
        private readonly Scale scale;
        //private int curString = 0;
        //private int curFret = 0;
        //private bool reverse = false;
        public bool[,] AvailableFrets { get; private set; } // Доступные лады в соответствии с гаммой и строем гитары

        public StringedVisualisation(StringedConfig instrument, StringedVisualizationConfig config, Scale scale)
        {
            Validate.IsTrue(instrument.Strings >= config.FirstString, "Настройка первой струны больше количества струн инструмента.");
            this.instrument = instrument;
            //this.config = config;
            this.scale = scale;
            //ActiveFrets = new bool[instrument.Strings, instrument.Frets + 1];
            Notes = new Note[instrument.Strings, instrument.Frets + 1];
            AvailableFrets = new bool[instrument.Strings, instrument.Frets + 1];
            InitializeNotes();
            InitializeAvailableFrets();
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

        private void InitializeAvailableFrets()
        {
            // Найти первые номера ладов всех струн, на которых совпадают ноты этих ладов и какой-либо ноты из гаммы
            for (int @string = 0; @string < instrument.Strings; @string++)
            {
                FindFirstCoincidentFretOnString(@string);
            }

            void FindFirstCoincidentFretOnString(int @string)
            {
                Note.NoteName fretNote = instrument.Tuning.Notes[@string].CurNote;
                for (int fret = 0; fret < 12; fret++, fretNote++) // на октаве по-любому найдётся совпадение с нотой гаммы
                {
                    Scale tempScale = new Scale(scale); // скопировать гамму, чтобы не измененять основную в цикле

                    foreach (Note scaleNote in tempScale)
                    {
                        if (fretNote == scaleNote.CurNote)
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
                Note.NoteName seachedNote = scale.GetCurrentNote().CurNote;

                for (int fret = startFret; fret < instrument.Frets; fret++)
                {
                    if (Notes[@string, fret].CurNote == seachedNote)
                    {
                        AvailableFrets[@string, fret] = true;
                        scale.MoveNext();
                        seachedNote = scale.GetCurrentNote().CurNote;
                    }
                }

            }
        }



        //public void StartExercice()
        //{
        //    Validate.IsTrue(config.FirstString != -1 && config.FirstFret != -1, "Не выбрана первая нота.");
        //    curFret = config.FirstFret;
        //    curString = config.FirstString;
        //}

        //private void SetNextFrets(int startString, int startFret, bool direction)
        //{
        //    int fretsOnString = 0;
        //    int fret = startFret;
        //    for (int fretInterval = 0; fretInterval < config.MaxInterval && fretsOnString < config.NotesOnString; fretInterval++)
        //    {
        //        if (availableFrets[startString, fret])
        //        {
        //            SetFret(startString, fret);
        //            fretsOnString++;
        //        }
        //        fret = direction ? ++fret : --fret;
        //    }
        //}

        //private void RemoveFretsOnString(int @string)
        //{
        //    for (int i = 0; i < instrument.Frets; i++)
        //    {
        //        ResetFret(@string, i);
        //    }
        //}

        //private void SetFret(int @string, int fret)
        //{
        //    ActiveFrets[@string, fret] = true;
        //}

        //private void ResetFret(int @string, int fret)
        //{
        //    ActiveFrets[@string, fret] = false;
        //}






        //private void FindAndSetFirstFret()
        //{
        //    for (int fret = 0; fret < instrument.Frets; fret++)
        //    {
        //        if (Notes[config.FirstString - 1, fret].CurNote == scale.Key)
        //        {
        //            curString = config.FirstString - 1;
        //            curFret = fret;
        //            SetFret(curString, curFret);
        //            return;
        //        }
        //    }
        //    throw new Exception("Нота не найдена.");
        //}

        //private void SetFirstFrets()
        //{
        //    int startPosFret = curFret;

        //    for (int i = 0; i < config.NotesOnNeck - 1; i++)
        //    {
        //        int interval = GetNextInterval(); ;

        //        if (NextStringCondition(startPosFret))
        //        {
        //            SetNextFretOnNextString(interval);
        //            startPosFret = curFret;
        //        }
        //        else
        //        {
        //            SetNextFret(interval);
        //        }
        //    }
        //}

        //public void SetFretsNextString()
        //{
        //    int @string = reverse ? curString - 1 : curString + 1;
        //    RemoveFretsOnString(@string);
        //    //int interval = GetNextInterval();

        //    if (CheckEndString())
        //    {
        //        RemoveFretsOnString(curString);

        //        if (!reverse)
        //        {
        //            SetNextFret(GetNextInterval());
        //            GetNextInterval(); 
        //            reverse = !reverse;
        //        }
        //        else
        //        {
        //            reverse = !reverse;
        //            SetNextFret(GetNextInterval());
        //            GetNextInterval();
        //        }                
        //        SetFirstFrets();
        //    }
        //    else
        //    {
        //        int interval = GetNextInterval();
        //        SetNextFretOnNextString(interval);

        //        int startPosFret = curFret;
        //        while (!NextStringCondition(startPosFret))
        //        {
        //            interval = GetNextInterval();
        //            SetNextFret(interval);
        //        }
        //    }           
        //}

        //private void RemoveFretsOnString(int @string)
        //{
        //    for (int i = 0; i < instrument.Frets; i++)
        //    {
        //        ResetFret(@string, i);
        //    }
        //}

        //private int FindFretByNote(Note note, int @string)
        //{
        //    if (!reverse)
        //    {
        //        for (int fret = curFret - config.MaxInterval - 1; fret <= curFret; fret++)
        //        {
        //            if (Notes[@string, fret] == note)
        //            {
        //                return fret;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int fret = curFret + config.MaxInterval + 1; fret >= curFret && fret < instrument.Frets; fret--)
        //        {
        //            if (Notes[@string, fret] == note)
        //            {
        //                return fret;
        //            }
        //        }
        //    }

        //    throw new Exception("В заданном диапазоне нота не найдена. Измените настройки диапазона.");
        //}

        //private void SetNextFretOnNextString(int interval)
        //{
        //    Note neededNote = new Note(Notes[curString, curFret]);
        //    neededNote.OffsetNote(interval, reverse);
        //    JumpToNextString();
        //    curFret = FindFretByNote(neededNote, curString);
        //    SetFret(curString, curFret);
        //}

        //private void SetNextFret(int interval)
        //{
        //    JumpToNextFret(interval);
        //    SetFret(curString, curFret);
        //}

        //private void JumpToNextFret(int interval)
        //{
        //    curFret = reverse ? curFret - interval : curFret + interval;
        //}

        //private void JumpToNextString()
        //{
        //    curString = reverse ? ++curString : --curString;
        //}

        //private bool CheckEdgeOfNeck() => curFret > instrument.Frets || curFret < 0;

        //private bool CheckEndString() => curString >= instrument.Strings - 1 || curString <= 0;

        //private bool NextStringCondition(int startPosFret) => CheckMaxInterval(startPosFret) || CheckNotesOnString();

        //private bool CheckMaxInterval(int startPosFret) => reverse ? startPosFret - curFret >= config.MaxInterval : curFret - startPosFret >= config.MaxInterval;

        //private bool CheckNotesOnString()
        //{
        //    int activeFretsCount = 0;

        //    for (int fret = 0; fret < instrument.Frets; fret++)
        //    {
        //        if (ActiveFrets[curString, fret])
        //        {
        //            activeFretsCount++;
        //        }
        //    }

        //    if (activeFretsCount >= config.NotesOnString)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //private int GetNextInterval() => reverse ? scale.MovePrevious() : scale.MoveNext();

        //private void SetFret(int @string, int fret)
        //{
        //    ActiveFrets[@string, fret] = true;
        //}

        //private void ResetFret(int @string, int fret)
        //{
        //    ActiveFrets[@string, fret] = false;
        //}

    }
}

