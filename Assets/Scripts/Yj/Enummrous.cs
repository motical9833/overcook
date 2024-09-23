using System.Collections.Generic;
using UnityEngine;

namespace Enummrous
{
    public enum PlayerAnimState
    {
        None,
        Idle,
        Walk,
        Hold,
        HoldWalk,
        Dead,
        Wash,
        Chop,
        End
    }

    public enum GrabState
    {
        Release,
        Grab,
        End
    }

    public enum IngredientSort
    {
        None,
        Onion,
        Beef,
        Mushroom,
        End
    }

    public static class IngredientInfo
    {
        public static List<IngredientElements> Ingredients = new List<IngredientElements>();

        static IngredientInfo()
        {
            Ingredients.Add(new IngredientElements("None", false, false, "", ""));
            Ingredients.Add(new IngredientElements("Onion", true, false, "2D/Food/Food_Icon/Onion_Icon","3D/Food_Objects/Onion/Onion"));
            Ingredients.Add(new IngredientElements("Meat_Raw", false, true ,"2D/Food/Food_Icon/Meat_Icon", "3D/Food_Objects/Meat_Raw/Meat_Raw"));
            Ingredients.Add(new IngredientElements("Mushroom", true, false, "2D/Food/Food_Icon/Mushroom_Icon", "3D/Food_Objects/Mushroom/Mushroom"));
        }

        public struct IngredientElements
        {
            public string name;
            private bool isBoiledAble;
            public bool isFriedAble;
            public string reousrce2DPath;
            public string reousrce3DPath;
            public IngredientElements(string _name, bool _isBoiledAble, bool _isFriedAble, string _reousrce2DPath ,string _reousrce3DPath)
            {
                name = _name;
                isBoiledAble = _isBoiledAble;
                isFriedAble = _isFriedAble;
                reousrce2DPath = _reousrce2DPath;
                reousrce3DPath = _reousrce3DPath;
            }

            public void SetBoiledAble(bool able)
            {
                if (able)
                    isBoiledAble = true;
                else
                    isBoiledAble = false;
            }

            public void SetFriedAble(bool able)
            {
                if (able)
                    isFriedAble = true;
                else
                    isFriedAble = false;
            }

            public bool GetIsBoiled()
            {
                return isBoiledAble;
            }

        }

    }


    public enum BoiledAbleIngredientSort
    {
        None,
        Onion,
        Mushroom,
        End
    }
    
    public enum FriedAbleIngredientSort
    {
        None,
        Beef,
        End
    }

    public static class CollisionCheck
    {
        public static bool Check(Bounds bounds, GameObject other)
        {
            Collider[] hitColliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity);

            foreach (Collider collider in hitColliders)
            {
                if (collider.transform.gameObject == other)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
