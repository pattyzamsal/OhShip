/* 
 * LudopiaTools uses EasyObjectPool library from marchingbytes for the easy use of the OjectPool Pattern,
 * here is the license use:
 *
 * Unless otherwise licensed, this file cannot be copied or redistributed in any format without the explicit consent of the author.
 * (c) Preet Kamal Singh Minhas, http://marchingbytes.com
 * contact@marchingbytes.com
 *
 * Revised and Modified by David Marull and used with the author consent
 * david.marull@gmail.com
 * davidm@ludopia.net
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LudopiaTools {

    /**
    * @description: Info specified in the inspector of the prefab EasyObjectPool
    */
    [System.Serializable]
    public class PoolInfo {
        public string poolName;
        public GameObject prefab;
        public int poolSize;
        public bool fixedSize;

        public PoolInfo( string poolName, GameObject prefab, int poolSize, bool fixedSize) {
            this.poolName = poolName;
            this.prefab = prefab;
            this.poolSize = poolSize;
            this.fixedSize = fixedSize;
        }
    }

    /**
    * @author: Preet Kamal Singh Minhas, contact@marchingbytes.com
    * @description: Defines a Pool of objects, threats each pool individually
    * @lastModifiedBy: David Marull, davidm@ludopia.net, david.marull@gmail.com
    */
    class Pool {

        private Stack<PoolObject> _availableObjStack = new Stack<PoolObject>();

        private List<GameObject> _activeList = new List<GameObject>();

        public Stack<PoolObject> GetObjStack {
            get { return _availableObjStack; }
        }

        public List<GameObject> GetActiveList {
            get { return _activeList; }
        }

        private bool fixedSize;
        private GameObject poolObjectPrefab;
        private int poolSize;
        private string poolName;

        private Transform poolRoot;

        public Pool( string poolName, GameObject poolObjectPrefab, int initialCount, bool fixedSize, Transform pool ) {
            this.poolName = poolName;
            this.poolObjectPrefab = poolObjectPrefab;
            this.poolSize = initialCount;
            this.fixedSize = fixedSize;
            this.poolRoot = pool;
            //populate the pool
            for( int index = 0; index < initialCount; index++ ) {
                AddObjectToPool( NewObjectInstance() );
            }
        }

        /// <summary>
        /// Add PoolObject to pool
        /// </summary>
        /// <param name="po"> PoolObject to Add to Pool </param>
        /**
        * @param po: PoolObject to Add to Pool
        * @description: Add PoolObject to pool
        */
        private void AddObjectToPool( PoolObject po ) {
            po.gameObject.SetActive( false );
            _availableObjStack.Push( po );
            po.isPooled = true;
            po.transform.SetParent( poolRoot, false );
        }

        /// <summary>
        /// Creates a new instance of the pool object type
        /// </summary>
        /// <returns> GameObject from the pool prefab with PoolObject component attached </returns>
        /**
        * @description: Creates a new instance of the pool object type
        * @returns: GameObject from the pool prefab with PoolObject component attached
        */
        private PoolObject NewObjectInstance() {
            GameObject go = (GameObject)GameObject.Instantiate( poolObjectPrefab );
            PoolObject po = go.GetComponent<PoolObject>();
            if( po == null ) {
                po = go.AddComponent<PoolObject>();
            }
            //set name
            po.poolName = poolName;
            return po;
        }

        /// <summary>
        /// Gets an available GameObject from the pool
        /// </summary>
        /// <param name="position"> Position where the game Object is going to be </param>
        /// <param name="rotation"> Rotation which the GameObject is going to take </param>
        /// <returns> Returns an inactive GameObject from the pool </returns>
        /**
        * @param position: Position where the game Object is going to be
        * @param rotation: Rotation which the GameObject is going to take
        * @returns: Returns an inactive GameObject from the pool
        * @description: Gets an available GameObject from the pool
        */
        public GameObject NextAvailableObject( Vector3 position, Quaternion rotation ) {
            PoolObject po = null;
            if( _availableObjStack.Count > 0 ) {
                po = _availableObjStack.Pop();
            }
            else if( fixedSize == false ) {
                //increment size var, this is for info purpose only
                poolSize++;
                Debug.Log( string.Format( "Growing pool {0}. New size: {1}", poolName, poolSize ) );
                //create new object
                po = NewObjectInstance();
            }
            else {
                Debug.LogWarning( "No object available & cannot grow pool: " + poolName );
            }

            GameObject result = null;
            if( po != null ) {
                po.isPooled = false;
                result = po.gameObject;
                result.SetActive( true );

                result.transform.position = position;
                result.transform.rotation = rotation;

                _activeList.Add( po.gameObject );
            }

            return result;
        }

        /// <summary>
        /// Gets an available GameObject from the pool
        /// </summary>
        /// <returns> Returns a GameObject from the pool </returns>
        /**
        * @returns: Gets the next available GameObject from the pool
        * @description: Gets an available GameObject from the pool
        */
        public GameObject NextAvailableObject() {
            PoolObject po = null;
            if( _availableObjStack.Count > 0 ) {
                po = _availableObjStack.Pop();
            }
            else if( fixedSize == false ) {
                //increment size var, this is for info purpose only
                poolSize++;
                Debug.Log( string.Format( "Growing pool {0}. New size: {1}", poolName, poolSize ) );
                //create new object
                po = NewObjectInstance();
            }
            else {
                Debug.LogWarning( "No object available & cannot grow pool: " + poolName );
            }

            GameObject result = null;
            if( po != null ) {
                po.isPooled = false;
                result = po.gameObject;
                result.SetActive( true );
                _activeList.Add( po.gameObject );
            }

            return result;
        }

        /// <summary>
        /// Returns an active PoolObject to his pool
        /// </summary>
        /// <param name="po"> PoolObject to return </param>
        /**
        * @param po: PoolObject to return
        * @returns: Returns an active PoolObject to his pool
        * @description: Returns an active PoolObject to his pool
        */
        public void ReturnObjectToPool( PoolObject po ) {

            if( poolName.Equals( po.poolName ) ) {

                /* we could have used availableObjStack.Contains(po) to check if this object is in pool.
				 * While that would have been more robust, it would have made this method O(n) 
				 */
                if( po.isPooled ) {
                    Debug.LogWarning( po.gameObject.name + " is already in pool. Why are you trying to return it again? Check usage." );
                }
                else {
                    _activeList.Remove( po.gameObject );
                    AddObjectToPool( po );
                }

            }
            else {
                Debug.LogError( string.Format( "Trying to add object to incorrect pool {0} {1}", po.poolName, poolName ) );
            }
        }
    }

    /// <summary>
    /// Script attached to the EasyObjectPool prefab for the easy use and creation of GameObjects in the different Pools
    /// </summary>
    /**
    * @description: Script attached to the EasyObjectPool prefab for the easy use and creation of GameObjects in the different Pools
    */
    public class EasyObjectPool : MonoBehaviour {

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        private static EasyObjectPool _instance;
        public static EasyObjectPool instance {
            get {
                if( _instance == null ) {
                    _instance = GameObject.FindObjectOfType( typeof( EasyObjectPool ) ) as EasyObjectPool;

                    if( _instance == null ) {
                        //No, "go" isn't named after Pokemon GO, is named after "GameObject"... Just in case...
                        GameObject go = new GameObject( "EasyObjectPool" );
                        _instance = go.AddComponent<EasyObjectPool>();
                        DontDestroyOnLoad( go );
                        _instance = go.GetComponent<EasyObjectPool>();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets a List of the currently used objects
        /// </summary>
        /// <param name="poolName"> Name ID of the used pool </param>
        /// <returns> List<GameObject> </returns>
        /**
        * @returns: List<GameObject>
        * @description: Gets a List of the currently used pools
        */
        public List<GameObject> GetActivePoolList( string poolName ) {
            if( poolDictionary.ContainsKey( poolName ) ) return poolDictionary[poolName].GetActiveList;
            else return null;
        }

        [Header( "Editing Pool Info value at runtime has no effect (working on it)" )]
        public List<PoolInfo> poolInfo;

        /// <summary>
        /// Mapping of pool name vs list
        /// </summary>
        /// <returns> Dictionary<string, Pool> </returns>
        /**
        * @returns: Dictionary<string, Pool>
        * @description: Mapping of pool name vs list
        */
        private Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

        void Awake() {
            //set instance
            _instance = this;
            //check for duplicate names
            CheckForDuplicatePoolNames();
            //create pools
            CreatePools();
        }

        /// <summary>
        /// Creates a new Pool with the specified GameObject type.
        /// </summary>
        /**
        * @description: Creates a new Pool with the specified GameObject type.
        */
        //TODO:
        //EasyObjectPool should refresh PoolInfo with the new Pool created
        public void CreateNewPoolInScene(string poolName, GameObject objectToAdd, int poolSize, bool isFixed ) {
            Pool pool = new Pool( poolName, objectToAdd,
                                    poolSize, isFixed, transform );
            //add to mapping dict
            poolInfo.Add( new PoolInfo(poolName, objectToAdd, poolSize, isFixed) );
            poolDictionary[poolName] = pool;
        }

        /// <summary>
        /// Re-creates all pools (it deletes existing pools).
        /// It's a taxing function call, be carefull.
        /// </summary>
        /**
        * @description: Re-creates all pools (it deletes existing pools).
        * It's a taxing function call, be carefull.
        */
        public void ReCreatePools() {
            foreach(Pool pool in poolDictionary.Values) {
                foreach( PoolObject go in pool.GetObjStack) {
                    Destroy( go.gameObject );
                }
            }
            poolInfo.Clear();
            //clears the dictionary values
            poolDictionary.Clear();
            //check for duplicate names
            CheckForDuplicatePoolNames();
            //create pools
            CreatePools();
        }

        /// <summary>
        /// Checks for duplicate pool names
        /// </summary>
        /**
        * @description: Checks for duplicate pool names
        */
        private void CheckForDuplicatePoolNames() {
            for( int index = 0; index < poolInfo.Count; index++ ) {
                string poolName = poolInfo[index].poolName;
                if( poolName.Length == 0 ) {
                    Debug.LogError( string.Format( "Pool {0} does not have a name!", index ) );
                }
                for( int internalIndex = index + 1; internalIndex < poolInfo.Count; internalIndex++ ) {
                    if( poolName.Equals( poolInfo[internalIndex].poolName ) ) {
                        Debug.LogError( string.Format( "Pool {0} & {1} have the same name. Assign different names.", index, internalIndex ) );
                    }
                }
            }
        }

        /// <summary>
        /// Create Pools and adds them to the dictionary
        /// </summary>
        /**
        * @description: Create Pools and adds them to the dictionary
        */
        private void CreatePools() {
            foreach( PoolInfo currentPoolInfo in poolInfo ) {

                Pool pool = new Pool( currentPoolInfo.poolName, currentPoolInfo.prefab,
                                     currentPoolInfo.poolSize, currentPoolInfo.fixedSize, transform );
                //add to mapping dict
                poolDictionary[currentPoolInfo.poolName] = pool;
            }
        }

        /// <summary>
        /// Returns an available object from the pool 
        /// OR 
        /// null in case the pool does not have any object available & can grow size is false.
        /// </summary>
        /// <param name="poolName"> Name of the Pool to retrieve a PoolObject prefab from </param>
        /// <param name="position"> Position where do you want to add the PoolObject </param>
        /// <param name="rotation"> Rotation orientation you want for the PoolObject </param>
        /// <returns> GameObject </returns>
        /**
        * @param poolName: Name of the Pool to retrieve a PoolObject prefab from
        * @param position: Position where do you want to add the PoolObject
        * @param rotation: Rotation orientation you want for the PoolObject
        * @description: Returns an available object from the pool 
        *               OR 
        *               null in case the pool does not have any object available & can grow size is false.
        * @returns: GameObject
        */
        public GameObject GetObjectFromPool( string poolName, Vector3 position, Quaternion rotation ) {
            GameObject result = null;

            if( poolDictionary.ContainsKey( poolName ) ) {
                Pool pool = poolDictionary[poolName];
                result = pool.NextAvailableObject( position, rotation );
                //scenario when no available object is found in pool
                if( result == null ) {
                    Debug.LogWarning( "No object available in pool. Consider setting fixedSize to false.: " + poolName );
                }

            }
            else {
                Debug.LogError( "Invalid pool name specified: " + poolName );
            }

            return result;
        }

        /// <summary>
        /// Returns an available object from the pool 
        /// OR 
        /// null in case the pool does not have any object available & can grow size is false.
        /// </summary>
        /// <param name="poolName"> Name of the Pool to retrieve a PoolObject prefab from </param>
        /// <returns> GameObject </returns>
        /**
        * @param poolName: Name of the Pool to retrieve a PoolObject prefab from
        * @description: Returns an available object from the pool 
        *               OR 
        *               null in case the pool does not have any object available & can grow size is false.
        * @returns: GameObject
        */
        public GameObject GetObjectFromPool( string poolName ) {
            GameObject result = null;

            if( poolDictionary.ContainsKey( poolName ) ) {
                Pool pool = poolDictionary[poolName];
                result = pool.NextAvailableObject();
                //scenario when no available object is found in pool
                if( result == null ) {
                    Debug.LogWarning( "No object available in pool. Consider setting fixedSize to false.: " + poolName );
                }

            }
            else {
                Debug.LogError( "Invalid pool name specified: " + poolName );
            }

            return result;
        }

        /// <summary>
        /// Returns a GameObject to a pool
        /// </summary>
        /// <param name="go"> GameObject to return to his pool (has to be a PoolObject too) </param>
        /**
        * @param go:  GameObject to return to his pool (has to be a PoolObject too)
        * @description: Returns a GameObject to a pool
        */
        public void ReturnObjectToPool( GameObject go ) {
            PoolObject po = go.GetComponent<PoolObject>();
            if( po == null ) {
                Debug.LogWarning( "Specified object is not a pooled instance: " + go.name );
            }
            else {
                if( poolDictionary.ContainsKey( po.poolName ) ) {
                    Pool pool = poolDictionary[po.poolName];
                    pool.ReturnObjectToPool( po );
                }
                else {
                    Debug.LogWarning( "No pool available with name: " + po.poolName );
                }
            }
        }
    }
}
