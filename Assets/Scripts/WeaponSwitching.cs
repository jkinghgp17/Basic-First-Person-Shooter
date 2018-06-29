using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

    public int selecctedWeapon = 0;

    public GameObject player;

    // Use this for initialization
    void Start()
    {
        SelectWeapon();
    }
	
	// Update is called once per frame
	void Update () {
        int previousSelectWeapon = selecctedWeapon;

		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selecctedWeapon >= transform.childCount - 1)
                selecctedWeapon = 0;
            else
                selecctedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selecctedWeapon <= 0)
                selecctedWeapon = transform.childCount - 1;
            else
                selecctedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selecctedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selecctedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selecctedWeapon = 2;
        }

        if (previousSelectWeapon != selecctedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        IWeapon selectedWeaponObject = null;
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selecctedWeapon)
            {
                weapon.gameObject.SetActive(true);
                selectedWeaponObject = weapon.GetComponent<IWeapon>();
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
        player.GetComponent<PlayerController>().weapon = selectedWeaponObject;
    }
}
