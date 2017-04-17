<?php

	// 1h

	$data = $_GET['code'];
//	var_dump($data);

	class CodeAnalyser
	{
		public $variables = array();
		public $loops = [
			'while' => array(),
			'for' => array(),
			'foreach' => array()
		];
		public $conditionals = array();

		public function __construct() { }

	}

	$obj = new CodeAnalyser();
	$matchesArr = array();


	preg_match_all('/(\$\w[\w|\d]*)/', $data, $matchesArr);
//	print_r($matchesArr);

	foreach ($matchesArr[0] as $value) {
		if (!isset($obj->variables[$value])) {
			$obj->variables[$value] = 0;
		}
		$obj->variables[$value]++;
	}

	preg_match_all("/(for\s*\(.*\))/", $data, $matchesArr);
//	print_r($matchesArr);

	foreach ($matchesArr[0] as $value) {
		$obj->loops['for'][] = $value;
	}

	preg_match_all("/(while\s*\(.*\))/", $data, $matchesArr);
//	print_r($matchesArr);

	foreach ($matchesArr[0] as $value) {
		$obj->loops['while'][] = $value;
	}

	preg_match_all("/(foreach\s*\(.*\))/", $data, $matchesArr);
//	print_r($matchesArr);

	foreach ($matchesArr[0] as $value) {
		$obj->loops['foreach'][] = $value;
	}

	preg_match_all("/((?:if|else\s*if)\s*\(.*\))/", $data, $matchesArr);
//	print_r($matchesArr);

	foreach ($matchesArr[0] as $value) {
		$obj->conditionals[] = $value;
	}
/*
	var_dump($obj->variables);
	echo "<br/>";
	var_dump($obj->loops);
	var_dump($obj->conditionals);
*/

	echo json_encode($obj);

