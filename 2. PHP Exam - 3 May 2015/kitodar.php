<?php

	// 37min

	$data = $_GET['data'];
	$dataSplitter = split(', ', $data);
	$typeOfOre = [];
	$quantity = [];
	$quantity['gold'] = 0;
	$quantity['silver'] = 0;
	$quantity['diamonds'] = 0;

	foreach ($dataSplitter as $value) {
		$miniSplit = split(' ', $value);
		if (strtolower($miniSplit[0]) == 'mine') {
			if (!in_array($miniSplit[2], $typeOfOre)) {
				$typeOfOre[] = $miniSplit[2];
			}
			if (!isset($quantity[strtolower($miniSplit[2])])) {
				$quantity[strtolower($miniSplit[2])] = 0;
			}
			$quantity[strtolower($miniSplit[2])] += intval($miniSplit[3]);
		}
	}

	//print_r($quantity);
		

	echo "<p>*Gold: {$quantity['gold']}</p>";
	echo "<p>*Silver: {$quantity['silver']}</p>";
	echo "<p>*Diamonds: {$quantity['diamonds']}</p>";



