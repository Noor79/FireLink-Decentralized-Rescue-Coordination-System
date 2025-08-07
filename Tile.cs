
namespace Wpf_Rescuemission
{
    // Julia Tadic 2024-11-17

    // Enum for describing the direction of walls surrounding a tile
    public enum WallDirection
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        Left = 4,
        Right = 8
    }

    // Class for the Tile
    public class Tile
    {
        // Define the delegate for the event
        public delegate void TileEventHandler(object sender, TileEventArgs args);

        // Event that other classes can subscribe to
        public event TileEventHandler TileChanged;
        public event TileEventHandler ObjectAdded;
        
        private bool _isFire;
        private bool _isSmoke;
        private bool _isMaterial;
        private bool _isPerson;
        private bool _hasFirefighter;

        // List for the image paths to be displayed on the tile
        public List<String> ImagePaths = new List<string>();
        public WallDirection Walls { get; set; }
        public int Row { get; } 
        public int Column { get; }


        // Constructor for the Tile
        public Tile(int row, int column)
        {
            Row = row;
            Column = column;
            _isFire = false;
            _isSmoke = false;
            _isMaterial = false;
            _isPerson = false;
        }

        public void setHazmat(bool value)
        {
            if (value && !IsMaterial)
            {
                ImagePaths.Add("Pictures\\hazmat.png");
                _isMaterial = true;
            }
            else if (!value)
            {
                if (ImagePaths.Contains("Pictures\\hazmat.png"))
                {
                    ImagePaths.Remove("Pictures\\hazmat.png");
                }
            }
            TileChanged?.Invoke(this, new TileEventArgs(" ", this)); // Invoke the event 
        }

        public void setVictim(bool value)
        {
            if (value && !IsPerson)
            {
                ImagePaths.Add("Pictures\\POI_3.png");
                _isPerson = true; 
            }
            else if (!value)
            {
                if (ImagePaths.Contains("Pictures\\POI_3.png"))
                {
                    ImagePaths.Remove("Pictures\\POI_3.png");
                    _isPerson = false;
                }
            }
            TileChanged?.Invoke(this, new TileEventArgs(" ", this)); // Invoke the event 
        }

        public void setFire(bool value)
        {
            if (value && !IsFire)
            {
                ImagePaths.Add("Pictures\\fire.png");
                _isFire = true;
            }
            else
            {
                if (ImagePaths.Contains("Pictures\\fire.png"))
                {
                    ImagePaths.Remove("Pictures\\fire.png");
                    _isPerson = false;
                }
            }
            TileChanged?.Invoke(this, new TileEventArgs(" ", this)); // Invoke the event 
        }

        public void setSmoke(bool value)
        {
            if (value && !IsSmoke)
            {
                ImagePaths.Add("Pictures\\smoke.png");
                _isSmoke = true;
            }
            else
            {
                if (ImagePaths.Contains("Pictures\\smoke.png"))
                {
                    ImagePaths.Remove("Pictures\\smoke.png");
                    _isPerson = false;
                }
            }
            TileChanged?.Invoke(this, new TileEventArgs(" ", this)); // Invoke the event 
        }

        // Set/get for IsFire property, with event
        public bool IsFire
        {
            get { return _isFire; }
            set
            {
                if (_isFire != value) // Check if the value actually changes
                {
                    _isFire = value;
                    UpdateImagePaths("Pictures\\fire.png", value, "Fire ");
                }
            }
        }

        // Set/get for IsSmoke property, with event
        public bool IsSmoke
        {
            get { return _isSmoke; }
            set
            {
                if (_isSmoke != value) // Check if the value actually changes
                {
                    _isSmoke = value;
                    UpdateImagePaths("Pictures\\smoke.png", value, "Smoke ");
                }
            }
        }

        // Set/get for IsMaterial property, with event
        public bool IsMaterial
        {
            get { return _isMaterial; }
            set
            {
                if (_isMaterial != value) // Check if the value actually changes
                {
                    _isMaterial = value;
                    UpdateImagePaths("Pictures\\hazmat.png", value, "Hazmat ");
                }
            }
        }

        // Set/get for IsPerson property, with event
        public bool IsPerson
        {
            get { return _isPerson; }
            set
            {
                if (_isPerson != value) // Check if the value actually changes
                {
                    _isPerson = value;
                    UpdateImagePaths("Pictures\\POI_3.png", value, "Victim ");
                }
            }
        }


        // Set/get for HasFirefighter property
        public bool HasFirefighter
        {
            get { return _hasFirefighter; }
            set
            {
                _hasFirefighter = value;

                if (value)
                {
                    ImagePaths.Add("Pictures\\pngegg.png"); // Lägg till bilden
                }
                else if (ImagePaths.Contains("Pictures\\pngegg.png"))
                {
                    ImagePaths.Remove("Pictures\\pngegg.png"); // Ta bort bilden
                }
                if (!ImagePaths.Contains("Pictures\\pngegg.png"))
                {
                    _hasFirefighter = false; 
                }
                TileChanged?.Invoke(this, new TileEventArgs(" ", this));
            }
        }

        private void UpdateImagePaths(string path, bool add, string variable)
        {
            if (add)
            {
                ImagePaths.Add(path);
                ObjectAdded?.Invoke(this, new TileEventArgs(variable, this));
            }
            else
            {
                if (ImagePaths.Contains(path))
                {
                    ImagePaths.Remove(path);
                }
            }
            TileChanged?.Invoke(this, new TileEventArgs(" ", this)); // Invoke the event 
        }
    }
}
