using SRV.BlastSnipe.Enemy;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SRV.BlastSnipe.Player.Gun {
    public class Weapon : MonoBehaviour {
        [SerializeField]
        private Button _shoot;

        [SerializeField]
        private Transform _shootPoint;

        private GunImprovement _gunImprovement;

        private LineRenderer _lineRenderer;

        private int _ammo;

        private int _damage;

        public GunImprovement GunImprovement => _gunImprovement;

        public Action AmmoEnd {
            get;
            private set;
        }

        public Action ShootEvent {
            get;
            private set;
        }

        public Action KillEvent {
            get;
            private set;
        }

        public Action ColisionBarrierEvent {
            get; 
            private set;
        }

        public void Init(int ammo, int damage, Action ammoEnd, Action shoot, Action kill, Action barrier) {
            try {
                if(_lineRenderer == null)
                    _lineRenderer = GetComponent<LineRenderer>();

                ShootEvent = shoot;
                AmmoEnd = ammoEnd;
                _ammo = ammo;
                _damage = damage;
                KillEvent = kill;
                ColisionBarrierEvent = barrier;
            }
            catch (Exception ex) {
                Debug.LogException(ex);
            }
        }

        public void Shoot() {
            try {
                if (_ammo <= 0)
                   AmmoEnd.Invoke();
                _ammo--;
                ShootEvent.Invoke();

                Vector3 direction = _shootPoint.forward;
                Ray ray = new Ray(_shootPoint.position, direction);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 12)) {
                    _lineRenderer.SetPosition(0, ray.origin);
                    _lineRenderer.SetPosition(1, hit.point);
                    
                    Debug.Log($"Попал: {hit.collider.gameObject.name}");
                    if (hit.collider.gameObject.GetComponent<EnemyHealth>() != null) {
                        hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(_damage);
                    }
                    if (hit.collider.tag == "Barrier") {
                        ColisionBarrierEvent();
                    }
                }
                else {
                    _lineRenderer.SetPosition(0, ray.origin);
                    _lineRenderer.SetPosition(1, ray.origin + ray.direction * 10);
                    Debug.Log("Лошара");
                }
                _lineRenderer.enabled = true;
                StartCoroutine(HideAfterDelay());
            }
            catch (Exception ex) {
                Debug.LogException(ex);
            }
        }

        private IEnumerator HideAfterDelay() {
            yield return new WaitForSeconds(0.4f);
            _lineRenderer.enabled = false;
            yield return null;
        }
    }
}