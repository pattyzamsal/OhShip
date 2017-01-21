/* 
 * Unless otherwise licensed, this file cannot be copied or redistributed in any format without the explicit consent of the author.
 * (c) Preet Kamal Singh Minhas, http://marchingbytes.com
 * contact@marchingbytes.com
 *
 * Modified by David Marull and used with the author consent
 * davidm@ludopia.net, david.marull@gmail.com
 */
using UnityEngine;
using System.Collections;

namespace LudopiaTools {

    /// <summary>
    /// Identifies if the object is waiting in pool or is in use, all objects from pool has this component automatically
    /// </summary>
	public class PoolObject : MonoBehaviour {
        public int id;
        public string poolName;
        //defines whether the object is waiting in pool or is in use
        public bool isPooled;

        /// <summary>
        /// Executed when the object is called from the ObjectPool
        /// </summary>
        /// <param name="poolId"></param>
        public void OnObjectPooled( int poolId ) {
            this.id = poolId;
        }
    }
}
