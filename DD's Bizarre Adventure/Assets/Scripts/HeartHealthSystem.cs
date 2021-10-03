using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealthSystem
{
    private List<Heart> heartList;
    public event EventHandler OnDamaged;
    public event EventHandler OnDead;
    public event EventHandler OnHealed;

    public HeartHealthSystem(int heartAmount)
    {
        heartList = new List<Heart>();
        for (int i = 0; i< heartAmount; i++)
        {
            Heart heart = new Heart(0);
            heartList.Add(heart);
        }
    }

    public List<Heart> GetHeartList()
    {
        return heartList;
    }

    public void Damage()
    {
        // circle through the heart list
        for (int i = heartList.Count - 1; i >= 0; i--)
        {
            Heart heart = heartList[i];
            // if the current heart is full, set it to empty and break out of the loop
            if (heart.GetFragmentAmount() == 0)
            {
                heart.SetFragment(1);
                break;
            }
        }

        //heartList[heartList.Count - 1].SetFragment(1);

        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);

        if (IsDead())
        {
            if (OnDead != null) OnDead(this, EventArgs.Empty);
        }
    }

    public void Heal()
    {
        for(int i =0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];
            if (heart.GetFragmentAmount() == 1)
            {
                heart.SetFragment(0);
                break;
            }
        }
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return heartList[0].GetFragmentAmount() == 1;
    }

    public class Heart
    {
        private int fragment;

        public Heart(int fragment)
        {
            this.fragment = fragment;
        }

        public int GetFragmentAmount()
        {
            return fragment;
        }

        public void SetFragment(int fragment)
        {
            this.fragment = fragment;
        }

    }
}
