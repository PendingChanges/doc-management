
import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
    overwrite: true,
    schema: "https://localhost:7282/graphql",
    documents: "src/graphql/**/*.graphql",
    generates: {
        "src/graphql/generated.ts": {
            plugins: [
                "typescript",
                'typescript-operations',
                'typescript-apollo-angular'
            ]
        }
    }
};

export default config;
