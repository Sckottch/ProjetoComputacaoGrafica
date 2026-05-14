using UnityEngine;

public class WireBox : MonoBehaviour
{
    [SerializeField] private Door door; 

    private WireBoxButton firstButton;
    private WireBoxButton secondButton;

    private int wiresConnected = 0;

    public void ConnectWire(WireBoxButton button)
    {
        if (firstButton == null)
        {
            firstButton = button;
            firstButton.ActivateLight();
            return;
        } 
        else if (firstButton == button)
        {
            return;
        }

        if (secondButton == null)
        {
            secondButton = button;
            secondButton.ActivateLight();
        }

        CheckConnection();
    }

    private void CheckConnection()
    {
        if (firstButton == null || secondButton == null) return;

        if (firstButton.wireColor == secondButton.wireColor)
        {
            wiresConnected++;
            firstButton.Connected();
            secondButton.Connected();

            ConnectClear();

            if (wiresConnected >= 4)
            {
                GameManager.Instance.Puzzle2Completed();
                door.UnlockDoor();
            }

            return;
        }

        Clear();
    }

    private void ConnectClear()
    {
        firstButton = null;
        secondButton = null;
    }

    private void Clear()
    {
        firstButton.DeactivateLight();
        secondButton.DeactivateLight();
        ConnectClear();
    }
}
