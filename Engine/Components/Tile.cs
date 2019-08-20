using SFML.Graphics;

namespace Engine.Components
{
    public struct Tile
    {
        public uint Index;
        public int TileValue;
        public Vertex Vertex1;
        public Vertex Vertex2;
        public Vertex Vertex3;
        public Vertex Vertex4;

        public Tile(uint index) 
            : this()
        {
            Index = index;
        }

        public Tile(uint index
            , int tileNumber
            , Vertex vertex1, Vertex vertex2, Vertex vertex3, Vertex vertex4)
        {
            Index = index;
            TileValue = tileNumber;
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
            Vertex4 = vertex4;
        }
    }
}
