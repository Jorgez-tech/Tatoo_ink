module.exports = {
  extends: ['@commitlint/config-conventional'],
  rules: {
    'type-enum': [
      2,
      'always',
      [
        'feat',
        'fix',
        'refactor',
        'docs',
        'style',
        'chore',
        'perf',
        'test',
        'revert',
      ],
    ],
    'subject-case': [0], // Permitir cualquier caso en la descripción
  },
};
