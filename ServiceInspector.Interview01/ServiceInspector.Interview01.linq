<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

#nullable enable
void Main()
{
	var arr = new uint[] { 3, 1, 8, 5, 4 };
	var nToTest = (uint)10;
	CanBeSumOf(arr, nToTest).Dump($"{nameof(CanBeSumOf)}( [{string.Join(", ", arr)}], {nToTest}):");
}

bool CanBeSumOf<TNaturalNumber>(TNaturalNumber[] array, TNaturalNumber toCheck) 
	where TNaturalNumber : IUnsignedNumber<TNaturalNumber>, IComparisonOperators<TNaturalNumber, TNaturalNumber, bool>
{
	if (array == null)
		throw new ArgumentNullException($"{nameof(array)} can not be null");

	if (array.Length == 0)
		throw new ArgumentException($"{nameof(array)} is empty");

	var min = array.Min()!;
	
	if (toCheck < min)
		return false;

	if (array.Contains(toCheck))
		return true;

	int len = array.Length;
	var calcs = BigInteger.Pow(2, len) - 1;

	for (BigInteger i = 1; i <= calcs; i++)
	{
		var bytes = i.ToByteArray();
		//var bytes = BitConverter.GetBytes(i);
		var bitArr = new BitArray(bytes);

		TNaturalNumber sum = TNaturalNumber.Zero;
		
		for (int j = 0; j < bitArr.Count; j++)
		{
			if (bitArr[j])
				sum += array[j];
		}
		
		if (sum == toCheck)
			return true;
	}

	return false;
}

#nullable disable