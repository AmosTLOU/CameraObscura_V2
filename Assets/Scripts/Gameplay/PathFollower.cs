using PathCreation;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay {
    public class PathFollower : MonoBehaviour {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 2;
        public UnityEvent reachedEnd;
        
        private float _distanceTravelled;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        public void UpdatePath(PathCreator newPath){
            pathCreator = newPath;
            _distanceTravelled = newPath.path.GetClosestDistanceAlongPath(transform.position);
        }

        void Update()
        {
            if (pathCreator != null)
            {
                _distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
            }

            if(Vector3.Magnitude(transform.position - pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1)) < 0.0001f)
            {
                reachedEnd.Invoke();
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            _distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}