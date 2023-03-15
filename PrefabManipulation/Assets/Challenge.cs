using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    /*Today we are dipping our toes further into reflection as well as learning useful editor tools used to same time refactoring and generating resources
    The objectives are the following

        1) Create a menu item that generates 20 enemy prefabs and saves them to the project
            a) The enemy prefab contains the following script: BoxCollider, Rigidbody2D, SpriteRenderer, Transform and one randomly selected Old_AIBase child                
            b) Set the enum in the Old_AIBase to a random enum
            c) Link one random scriptable object to it
            d) The enemy AI script should have all elements properly linked
            e) Have a collection of words, and randomly generate a name by mixing 2 of the words
            f) Save the prefab to the project, refresh the asset database


        2) Create a menu item that fixes all the bad prefabs. It will loop through and replace all the bad scripts, but maintain the links and values
            a) Loop through all prefabs
            b) Remove the old component and replace it with the good one
            c) Be sure to maintain all links and values
            d) Save the prefab properly, handle all errors and edge cases

        3) Fix the bug
        a) When the game starts, load 10 of the prefabs randomly
        b) Press the "A" key on the keyboard to deal damage to the AI's
        c) Understand & Fix the bug
            
    

    Scalability points
    1a) Instead of hardcoding the components, make it automatically scale with the files in the folder
    b) Make it scalable with any enum
    c) Make it scalable with any number of scriptables in that folder
    f) Handle name uniqueness
    
    2) Be sure to debug log out status, changes, useful info. Employ defensive programming
    (Not currently possible with your knowledge) make it automatically map any variables with the same name



    /*
    The lecture will be very short
    It is to explain some of the libraries, and what is possible
    But the rest is for you to research and figure out


    */


    /*====   Useful libraries
     *   
     *   AssetDatabase
     *   PrefabUtility
     *   MenuItem
     *   System.IO.Directory
     *   System.IO.Path
     *   System.IO.File
     *   System.Type
     * 
     * 
     *======  Useful functions
     *  
     *  AssetDatabase.Refresh
     *  AssetDatabase.SaveAssets
     *  AssetDatabase.GetAssetPath
     *  AssetDatabase.DeleteAsset
     *  
     *  PrefabUtility.SaveAsPrefabAsset
     *         
     *  GameObject.DestroyImmediate
     *  
     *  Type.GetType
     * 
     */





}
