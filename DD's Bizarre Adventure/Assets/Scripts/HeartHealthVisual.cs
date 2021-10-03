using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealthVisual : MonoBehaviour
{
    public static HeartHealthSystem heartHealthSystemStatic;
    [SerializeField] private Sprite heartSprite0;
    [SerializeField] private Sprite heartSprite1;

    private List<HeartImage> heartImageList;
    private HeartHealthSystem heartHealthSystem;

    public PlayerMovement movement;
    public GameObject gameOverText, player, restartGame, quit;

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }
    private void Start()
    {
        gameOverText.SetActive(false);
        restartGame.SetActive(false);
        quit.SetActive(false);
        HeartHealthSystem heartHealthSystem = new HeartHealthSystem(movement.inihealth);
        SetHeartHealthSystem(heartHealthSystem);
    }

    public void SetHeartHealthSystem(HeartHealthSystem heartHealthSystem)
    {
        this.heartHealthSystem = heartHealthSystem;
        heartHealthSystemStatic = heartHealthSystem;
        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        Vector2 heartAnchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < heartList.Count; i++)
        {
            HeartHealthSystem.Heart heart = heartList[i];
            CreateHearts(heartAnchoredPosition).SetFragment(heart.GetFragmentAmount());
            heartAnchoredPosition += new Vector2(30, 0);
        }
        heartHealthSystem.OnDamaged += HeartHealthSystem_OnDamaged;
        heartHealthSystem.OnDead += HeartHealthSystem_OnDead;
        heartHealthSystem.OnHealed += HeartHealthSystem_OnHealed;
    }

    private void HeartHealthSystem_OnHealed(object sender, EventArgs e)
    {
        RefreshImage();
    }

    private void HeartHealthSystem_OnDead(object sender, EventArgs e)
    {
        Destroy(player);
        gameOverText.SetActive(true);
        restartGame.SetActive(true);
        quit.SetActive(true);
    }

    private void HeartHealthSystem_OnDamaged(object sender, EventArgs e)
    {
        RefreshImage();  
    }

    private void RefreshImage()
    {
        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        for (int i = 0; i < heartList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartHealthSystem.Heart heart = heartList[i];
            heartImage.SetFragment(heart.GetFragmentAmount());
        }
    }

    private HeartImage CreateHearts(Vector2 anchoredPosition)
    {
        // create game object
        GameObject heartObject = new GameObject("Heart", typeof(Image));

        // set as child of this transform
        //heartObject.transform.parent = transform;
        heartObject.transform.SetParent(transform);
        heartObject.transform.localPosition = Vector3.zero;

        // locate and size heart
        heartObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);

        // set heart sprite
        Image heartImageUI = heartObject.GetComponent<Image>();
        heartImageUI.sprite = heartSprite0;

        HeartImage heartImage = new HeartImage(this,heartImageUI);
        heartImageList.Add(heartImage);
        return heartImage;
    }

    public class HeartImage
    {
        private Image heartImage;
        private HeartHealthVisual heartHealthVisual;

        public HeartImage(HeartHealthVisual heartHealthVisual, Image heartImage)
        {
            this.heartHealthVisual = heartHealthVisual;
            this.heartImage = heartImage;
        }

        public void SetFragment (int fragment)
        {
            switch (fragment)
            {
                case 0: heartImage.sprite = heartHealthVisual.heartSprite0; break;
                case 1: heartImage.sprite = heartHealthVisual.heartSprite1; break;
            }
        }
    }
}
