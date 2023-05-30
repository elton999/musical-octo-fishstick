using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace UmbrellaToolsKit
{
    public class AssetManagement
    {
        public static AssetManagement Instance;
        public AssetManagement()
        {
            Instance = this;
        }
        public List<AssetObject> AssetsList = new List<AssetObject>();
        public List<AssetObject> LevelAssetsList = new List<AssetObject>();

        public void Set<T>(string tag, Layers layer) where T : GameObject
        {
            AssetObject assetObject = new AssetObject { Name = tag, Layer = layer, GameObject = typeof(T) };
            this.AssetsList.Add(assetObject);
        }

        public GameObject GetObject(string name)
        {
            IEnumerable<AssetObject> assetObjects = this.AssetsList.Where(asset => asset.Name == name);

            if (assetObjects.Count() > 0)
            {
                AssetObject assetObject = assetObjects.First();
                GameObject gameObject = (GameObject)Activator.CreateInstance(assetObject.GameObject);

                return gameObject;
            }

            return new GameObject();
        }

        public Layers GetLayer(string name)
        {
            IEnumerable<AssetObject> assetObjects = this.AssetsList.Where(asset => asset.Name == name);
            if (assetObjects.Count() > 0)
            {
                return assetObjects.ToList()[0].Layer;
            }
            return Layers.MIDDLEGROUND;
        }


        public void addEntityOnScene(string name, Vector2 position, Point size, Dictionary<string, string> values, List<Vector2> nodes, Scene scene)
        { // ? values:Dynamic, ? nodes:Array<Vector2>, ? flipx:Bool):Void{
            GameObject gameObject = this.GetObject(name);
            Layers layer = this.GetLayer(name);

            gameObject.Position = position;
            gameObject.size = size;
            gameObject.Values = values;
            gameObject.Nodes = nodes;

            scene.AddGameObject(gameObject, layer);
        }


        public void ClearAll()
        {
            this.LevelAssetsList.Clear();
        }
    }
}
