using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    public float spinSpeed = 500f; // �ִ� ȸ�� �ӵ�
    private float currentSpeed;
    private bool isSpinning = false;
    public Transform roulette;
    public Text scoerT;
    private int scoerCont = 0;

    void Update()
    {
        if (isSpinning)
        {
            roulette.Rotate(0, 0, currentSpeed * Time.deltaTime); // �귿 ȸ��

            // ������ ���� ���� (0.5~3 ���̿��� ���ӷ� ����)
            float randomDeceleration = Random.Range(0.5f, 3f);
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * randomDeceleration);

            if (currentSpeed < 1f) // ���� �� ó��
            {
                isSpinning = false;
                DetermineResult();
            }
        }
    }

    public void SpinRoulette()
    {
        if (!isSpinning)
        {
            currentSpeed = spinSpeed;
            isSpinning = true;
        }
    }

    void DetermineResult()
    {
        float angle = (roulette.eulerAngles.z + 24) % 360; // -24���� �����Ͽ� 0~360���� ����
          Debug.Log(angle);

        if (angle >= 0 && angle < 48)
            scoerCont += 50;
        else if (angle >= 48 && angle < 94)
            scoerCont += 100;
        else if (angle >= 94 && angle < 140)
            scoerCont += 1000;
        else if (angle >= 140 && angle < 183)
            scoerCont += 10;
        else if (angle >= 183 && angle < 222)
            scoerCont += 50;
        else if (angle >= 222 && angle < 267)
            scoerCont += 100;
        else if (angle >= 267 && angle < 310)
            scoerCont += 1;
        else
            scoerCont += 10;

       scoerT.text = scoerCont.ToString();

        if (scoerCont == 5000)
        { 
        
        }
    }
}
