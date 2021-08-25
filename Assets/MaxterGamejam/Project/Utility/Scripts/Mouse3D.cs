using System;
using UnityEngine;

namespace LOK1game.Tools
{
	public class Mouse3D
	{
        /// <summary>
        /// ¬озвращает позицию курсора в мировых координатах
        /// </summary>
        /// <returns>ѕозици€ курсора в мире</returns>
        public static Vector3 GetMouseWorldPosition()
        {
            if (Camera.main == null)
            {
                throw new NullReferenceException("There is no main camera");
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 1000f)) { return hit.point; }

            else { return Camera.main.transform.forward * 100f; }
        }
	}
}