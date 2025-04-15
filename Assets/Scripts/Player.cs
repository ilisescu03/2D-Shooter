using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

    public class Player : Character
    {
        [SerializeField]
        private int ControlsIndex;
        private bool AutoSave = false;
        private bool isUsingMinigun = false;
        [SerializeField]
        private int WaveIndex;
        [SerializeField]
        private int coins;
        [SerializeField]
        private HealthBar healthbar;
        [SerializeField]
        private int score;
        [SerializeField]
        private int high_score;
        private float fire_rate;
        private Shooting shooting;
        private bool canShoot = true;
        private bool isAlive = false;
        private bool invicibility = false;
        private float angle = 2.5f;
        [SerializeField]
        protected Vector2 spawnpoint;
        private int ammo;
        private int maxammo;
        private int LayerIndex = 0;
        [SerializeField]
        UIManager uiManager;
        [SerializeField]
        private GameOverMenu menu;
        [SerializeField]
        private Pause pause;
        [SerializeField]
        private EnemySpawner spawner;
        [SerializeField]
        private Weapon weapon;
        [SerializeField]
        private GameObject WeaponObject;
        [SerializeField]
        private float Offset;
        [SerializeField]
        private bool[] WeaponBools;
        [SerializeField]
        private WeaponsManager weaponsManager;
        [SerializeField]
        private int ammoPerRound;
        private int MAXValue;
        private float ShootingDamage;
        [SerializeField]
        private Timer timer;
        private bool InfiniteFire = false;
        private float mintime = 3.5f;
        private float maxtime = 7f;
        [SerializeField]
        private AudioClip gunShot;
        private int scoreCount = 0;
        private int scoreCount2 = 0;
        private int changeWave = 1200;
        [SerializeField]
        private AudioManager audioManager;
        [SerializeField]
        private CursorManager cursorManager;
        [SerializeField]
        private Button button;
        private Vector3 lastMousePosition;
        private bool isMouseMoving = false;
        private bool isInitialized = false;
        private bool isReloading = false;
        [SerializeField]
        private Keybinds keybinds;
        private bool countingDown = false;
        private float countdown = 0f;
        [SerializeField]
         private GameObject prefab;
        // Start is called before the first frame update
        void CheckMouseMovement()
        {
            if (Input.mousePosition != lastMousePosition)
            {
                isMouseMoving = true;
            }
            else
            {
                isMouseMoving = false;
            }
            lastMousePosition = Input.mousePosition;
        }
        public void set_InfiniteFire(bool value) { InfiniteFire = value; }
        public void set_ControlsIndex(int value) { if (value == 0 || value == 1) ControlsIndex = value; }
        public void setLayerIndex(int value1, int value2)
        {
            animator.SetLayerWeight(0, value1);
            animator.SetLayerWeight(1, value2);
            if (value1 > value2) LayerIndex = 0;
            else LayerIndex = 1;

        }
        public bool IsUsingMinigun()
        {
            return isUsingMinigun;
        }
        public void enableMinigun(bool state)
        {

            if (state) { animator.SetLayerWeight(1, 1); isUsingMinigun = true; WeaponObject.SetActive(false); }
            else { animator.SetLayerWeight(1, 0); isUsingMinigun = false; WeaponObject.SetActive(true); }
        }
        public bool nextWave()
        {
            if (scoreCount2 >= changeWave) return true;
            else return false;
        }
        public void set_AutoSave(bool value) { AutoSave = value; }
        public int get_Wave_Index() { return WaveIndex; }
        public int get_coins() { return coins; }
        public int get_score() { return score; }
        public int get_high_score() { return high_score; }
        public bool get_state() { return isAlive; }
        public void set_fire_rate(float value) { fire_rate = value; }
        public void set_offset(float value) { Offset = value; }
        public void setBool(int index) { WeaponBools[index] = true; }
        public void setDamage(float value) { ShootingDamage = value; }
        public Weapon getCurrentWeapon() { return weapon; }
        public void setNewWeapon(int value, int value2, Weapon newWeapon, GameObject _WeaponObject, AudioClip clip)
        {
            gunShot = clip;
            maxammo = value - value2;
            ammoPerRound = value2;
            ammo = ammoPerRound;
            weapon.Unassign();
            if (WeaponObject != null)
            {
                Destroy(WeaponObject);
            }


            WeaponObject = Instantiate(_WeaponObject, transform.position, Quaternion.identity);
            WeaponObject.transform.SetParent(transform);
            WeaponObject.transform.localPosition = Vector3.zero;
            WeaponObject.transform.localRotation = Quaternion.identity;
            WeaponObject.transform.localScale = new Vector3(-1, -1, 1);
            if (!WeaponObject.activeSelf) WeaponObject.SetActive(true);
            weapon = newWeapon;

        }

        public float get_fire_rate() { return fire_rate; }
        public void set_invicibility(bool value) { invicibility = value; }
        public void set_angle(int value)
        {
            angle += value;
        }
        public void AddCoins(int value)
        {
            coins += value;
            audioManager.PlayCollectSFX();
        }
        public bool RemoveCoins(int value)
        {

            if (value <= coins)
            {
                coins -= value;
                if (isInitialized) audioManager.PlayPurchaseSFX();
                return true;
            }
            else
            {
                uiManager.NotEnoughCoinsShow();
                return false;
            }
        }
        public float get_angle()
        {
            return angle;
        }
        public bool[] get_WeaponBools()
        {
            return WeaponBools;
        }
        bool IsCursorOverButton()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                if (result.gameObject == button.gameObject)
                    return true;
            }

            return false;
        }
        protected override void Start()
        {
            keybinds.InitializeKeys();
            cursorManager.SetCursorTexture(new Vector2(16, 16));
            uiManager.HideReloading();
            Time.timeScale = 0;
            MAXValue = Random.Range(75, 225);
            fire_rate = weapon.getFireRate();
            ShootingDamage = weapon.getDamage();
            // ammoPerRound = weapon.getAmmoPerRound();
            maxammo = weapon.getAmmo() - ammoPerRound;
            ammo = ammoPerRound;
            if (ammo == 0 && maxammo > 0)
            {
                Reload();
            }
            shooting = GetComponent<Shooting>();
            high_score = SaveManager.LoadHighScore();
            coins = SaveManager.LoadCoins();
            coins = 500;
            angle = SaveManager.LoadAngle();
            WeaponBools = SaveManager.LoadWeapons();
            if (WeaponBools == null)
            {
                WeaponBools = new bool[3];
            }
            for (int i = 0; i < WeaponBools.Length; i++)
            {
                if (WeaponBools[i]) weaponsManager.setActive(i);
            }
            uiManager.Set_Text(score, high_score, WaveIndex);
            uiManager.Set_Ammo_Text(ammo, maxammo);
            uiManager.HideCountdownText();
            isInitialized = true;
        }

        // Update is called once per frame
        protected override void Update()
        {

            CheckMouseMovement();
            if (isAlive && !pause.get_state() && ControlsIndex == 1 && !IsCursorOverButton()) cursorManager.SetTargetTexture(new Vector2(16, 16));
            if (isAlive && !pause.get_state() && ControlsIndex == 1 && IsCursorOverButton()) cursorManager.SetCursorTexture(new Vector2(16, 16));
            if (isAlive && !pause.get_state() && ControlsIndex == 0 && isMouseMoving) cursorManager.SetCursorTexture(new Vector2(16, 16));
            if (isAlive && !pause.get_state() && ControlsIndex == 0 && !isMouseMoving) cursorManager.SetNoCursorTexture(new Vector2(16, 16));

            if (!isAlive || pause.get_state()) cursorManager.SetCursorTexture(new Vector2(16, 16));
            if (AutoSave) { uiManager.Save(); }
            // else Debug.Log("Autosave is off");
            if (!InfiniteFire) uiManager.Set_Ammo_Text(ammo, maxammo);
            uiManager.Set_Text(score, high_score, WaveIndex);
            uiManager.SetCoinsText(coins);
            GetInput();
            base.Update();
             
            prefab.transform.position = transform.position;
            
        if (direction != Vector2.zero)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
            healthbar.fillAmount = health / maxhealth;
            if (countingDown)
            {
                countdown -= Time.deltaTime;
                uiManager.SetCountdownText(Mathf.CeilToInt(countdown));
                if (countdown <= 0f)
                {
                    countingDown = false;
                    WaveIndex++;
                    changeWave += 1200;
                    spawner.insertInVector();
                    mintime = 3f;
                    maxtime = 6f;
                    spawner.set_spawnTime(mintime, maxtime);
                    spawner.EnableSpawn();
                    uiManager.HideCountdownText();
                }
            }
            if (!isAlive) spawner.set_spawnTime(3.5f, 7f);

        }
        public void ResetAmmo()
        {
            ammo = weapon.getAmmoPerRound();
            maxammo = weapon.getAmmo() - weapon.getAmmoPerRound();
        }
        public void TakeDamage(float damage)
        {
            if (invicibility) return;
            health -= damage;
            StartCoroutine(DamageEffect());
            health = Mathf.Max(health, 0.0f);
            if (health == 0)
            {
                Die();
                menu.Show();
            }
        }
        public void Die()
        {
            isAlive = false;
            WaveIndex = 1;
            scoreCount2 = 0;
            scoreCount = 0;
            spawner.clearVector();
            changeWave = 1200;
            spawner.set_spawnTime(3.5f, 7f);
            spawner.ResetNumberOfZombies();
            mintime = 3.5f;
            maxtime = 7f;
            Debug.Log("Dead");

        }
        public void Heal(float points)
        {
            health += points;
            if (health > maxhealth) health = maxhealth;
        }
        public void Increase_Score(int points)
        {
            score += points;
            scoreCount += points;
            scoreCount2 += points;
            if (scoreCount >= MAXValue)
            {
                mintime /= 1.25f;
                maxtime /= 1.25f;
                if (mintime < 0.75f || maxtime < 0.75f)
                {
                    mintime = 0.75f;
                    maxtime = 0.75f;
                }
                scoreCount = 0;

                MAXValue = Random.Range(40, 100);
                spawner.set_spawnTime(mintime, maxtime);
            }
            if (scoreCount2 >= changeWave)
            {
                StartCountdown();
            }
            if (score > high_score) high_score = score;


        }
        private void StartCountdown()
        {
            countingDown = true;
            countdown = 10f;
            spawner.DisableSpawn();
            uiManager.ShowCountdownText();
            Enemy.ClearAll();
            spawner.ResetNumberOfZombies();
            scoreCount2 = 0;
        }
        public void Respawn()
        {
            isAlive = true;
            Time.timeScale = 1;
            menu.Hide();
            transform.position = new Vector2(spawnpoint.x, spawnpoint.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            health = maxhealth;
            ammo = weapon.getAmmoPerRound();
            maxammo = weapon.getAmmo() - ammo;
            if (ammo == 0 && maxammo > 0)
            {
                Reload();
            }
            WaveIndex = 1;
            score = 0;

            spawner.set_spawnTime(4, 8);
            uiManager.Set_Text(score, high_score, WaveIndex);
        }
        public void Reload()
        {
            if (maxammo > 0)
            {
                int ammoNeeded = ammoPerRound - ammo;
                if (maxammo >= ammoNeeded)
                {

                    maxammo -= ammoNeeded;
                    ammo += ammoNeeded;
                }
                else
                {
                    ammo += maxammo;
                    maxammo = 0;
                }
            }
            isReloading = false;
            uiManager.HideReloading();
        }
        public IEnumerator Reloading()
        {
            isReloading = true;
            uiManager.ShowReloading();
            yield return new WaitForSeconds(2f);
            Reload();
        }
        void GetInput()
        {
            // Primeste de la tastatura input
            if (isAlive)
            {
                direction = Vector2.zero;
                if (Input.GetKey(keybinds.keys["Up"]))
                {
                    direction += Vector2.up;
                }
                if (Input.GetKey(keybinds.keys["Down"]))
                {
                    direction += Vector2.down;
                }
                if (Input.GetKey(keybinds.keys["Left"]))
                {
                    direction += Vector2.left;
                }
                if (Input.GetKey(keybinds.keys["Right"]))
                {
                    direction += Vector2.right;
                }
                if (Input.GetKey(KeyCode.LeftArrow) && ControlsIndex == 0)
                {
                    transform.Rotate(0, 0, 3.5f);
                }
                if (Input.GetKey(KeyCode.RightArrow) && ControlsIndex == 0)
                {
                    transform.Rotate(0, 0, -3.5f);
                }
                if (ControlsIndex == 1 && !pause.get_state())
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0f;

                    Vector3 direction = mousePos - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
                }
                if (Input.GetKeyDown(KeyCode.R) && !Input.GetKey(KeyCode.Space) && !pause.get_state())
                {
                    audioManager.PlayReloadSFX();
                    StartCoroutine(Reloading());
                }
                if (ControlsIndex == 0 && Input.GetKey(KeyCode.Space) && !isReloading && ((canShoot && !pause.get_state() && !InfiniteFire) || (canShoot && InfiniteFire)))
                {
                    if (ammo > 0)
                    {
                        Debug.Log("FireRate:" + fire_rate);
                        if (!isUsingMinigun) audioManager.PlaySFX(gunShot);
                        else audioManager.PlayMinigun();
                        shooting.Shoot(Offset, ShootingDamage);
                        if (!InfiniteFire) ammo -= 1;
                        canShoot = false;
                        StartCoroutine(Timer());
                    }
                    if (ammo == 0 && isUsingMinigun)
                    {
                        audioManager.PlayMinigun();
                        shooting.Shoot(Offset, ShootingDamage);
                        canShoot = false;
                        StartCoroutine(Timer());
                    }
                    if (ammo == 0 && !isUsingMinigun)
                    {
                        audioManager.PlayEmptySFX();
                        canShoot = false;
                        StartCoroutine(Timer());
                    }
                }
                if (!IsCursorOverButton() && ControlsIndex == 1 && Input.GetMouseButton(0) && !isReloading && ((canShoot && !pause.get_state() && !InfiniteFire) || (canShoot && InfiniteFire)))
                {
                    if (ammo > 0)
                    {
                        Debug.Log("FireRate:" + fire_rate);
                        if (!isUsingMinigun) audioManager.PlaySFX(gunShot);
                        else audioManager.PlayMinigun();
                        shooting.Shoot(Offset, ShootingDamage);
                        if (!InfiniteFire) ammo -= 1;
                        canShoot = false;
                        StartCoroutine(Timer());
                    }
                    if (ammo == 0 && isUsingMinigun)
                    {
                        audioManager.PlayMinigun();
                        shooting.Shoot(Offset, ShootingDamage);
                        canShoot = false;
                        StartCoroutine(Timer());
                    }
                    if (ammo == 0 && !isUsingMinigun)
                    {
                        audioManager.PlayEmptySFX();
                        canShoot = false;
                        StartCoroutine(Timer());
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (pause.get_state() == false) pause.TogglePause();
                    else { pause.ResumeGame(); }
                }
            }
        }

        IEnumerator DamageEffect()
        {
            audioManager.PlayHurt();
            SpriteRenderer effect = GetComponent<SpriteRenderer>();
            effect.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            effect.color = Color.white;
        }
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(fire_rate);


            canShoot = true;

        }
        public void Reset_Data()
        {
            SaveManager.ResetData();
            high_score = 0;
            score = 0;
            uiManager.Set_Text(score, high_score, WaveIndex);
        }

    }
