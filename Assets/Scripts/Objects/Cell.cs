    public class Cell
    {
        private bool _visited = false;
        private bool[] _status = new bool[4];
        private int _xPos, _yPos;
        
        public Cell(int xPos, int yPos)
        {
            _xPos = xPos;
            _yPos = yPos;
            _visited = false;
            _status = new bool[4];
        }

        public void Reset() {
            _visited = false;
            _status = new bool[4];
        }

        public void SetVisited(bool visited)
        {
            this._visited = visited;
        }

        public bool GetVisited()
        {
            return _visited;
        }

        public void SetStatus(int index, bool status)
        {
            if (index < this._status.Length && index >= 0)
            {
                this._status[index] = status;
            }
        }

        public bool[] GetStatus()
        {
            return _status;
        }

        public void SetxPos(int xPos)
        {
            this._xPos = xPos;
        }

        public void SetyPos(int yPos)
        {
            this._yPos = yPos;
        }

        public int GetxPos()
        {
            return _xPos;
        }

        public int GetyPos()
        {
            return _yPos;
        }


    }