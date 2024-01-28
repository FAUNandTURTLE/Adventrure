using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInteract : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public HeroKnight player;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.ShieldDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {   
        player.ShieldUP();
    }
}
