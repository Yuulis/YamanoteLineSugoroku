using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RouletteController : MonoBehaviour
{
    [SerializeField] private int segmentsCount = 8;
    [SerializeField] private float spinDuration = 5.0f;
    [SerializeField] private float spinFirstSpeed = 500.0f;
    [SerializeField] private GameObject segmentObj;

    private GameObject canvas;
    private Image segmentImage;
    private TextMeshProUGUI segmentText;

    private bool isSpinning = false;

    void Start()
    {
        canvas = this.transform.GetChild(0).gameObject;
        segmentImage = segmentObj.GetComponent<Image>();
        segmentText = segmentObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        CreateRoulette();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpinning)
        {
            StartCoroutine(SpinRoulette());
        }
    }

    private void CreateRoulette()
    {
        float angleStep = 360.0f / segmentsCount;
        segmentImage.fillAmount = 1.0f / segmentsCount;
        for (int i = 0; i < segmentsCount; i++)
        {
            segmentImage.color = GetRandomVividColor();
            segmentText.SetText((i + 1).ToString());

            GameObject newSegment = Instantiate(segmentObj, transform);
            newSegment.transform.SetParent(canvas.transform, false);
            newSegment.transform.localRotation = Quaternion.Euler(0, 0, -i * angleStep);
        }
    }

    private IEnumerator SpinRoulette()
    {
        isSpinning = true;
        float currentSpinSpeed = spinFirstSpeed;
        float spinTime = 0f;

        while (currentSpinSpeed > 1.0f)
        {
            this.transform.Rotate(0, 0, -currentSpinSpeed * Time.deltaTime);
            currentSpinSpeed = Mathf.Lerp(currentSpinSpeed, 0, spinTime / spinDuration);
            spinTime += Time.deltaTime;
            yield return null;
        }

        isSpinning = false;

        int selectedSegment = Mathf.FloorToInt((this.transform.eulerAngles.z % 360) / (360f / segmentsCount));
        Debug.Log($"Selected segment: {selectedSegment + 1}");
    }

    private Color GetRandomVividColor()
    {
        Color color;
        float random = Random.value;

        if (random < 1f / 6f)
        {
            color = new(0f, 1f, Random.value);
        }
        else if (random < 2f / 6f)
        {
            color = new(0f, Random.value, 1f);
        }
        else if (random < 3f / 6f)
        {
            color = new(Random.value, 0f, 1f);
        }
        else if (random < 4f / 6f)
        {
            color = new(1f, 0f, Random.value);
        }
        else if (random < 5f / 6f)
        {
            color = new(1f, Random.value, 0f);
        }
        else
        {
            color = new(Random.value, 1f, 0f);
        }

        return color;
    }
}
